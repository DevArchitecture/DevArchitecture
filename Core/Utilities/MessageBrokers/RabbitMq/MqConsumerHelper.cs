using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.Utilities.MessageBrokers.RabbitMq
{
    public class MqConsumerHelper : IMessageConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly MessageBrokerOptions _brokerOptions;

        public MqConsumerHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _brokerOptions = _configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
        }

        public void GetQueue()
        {
            ConnectionFactory factory = new ConnectionFactory();

            factory.UserName = _brokerOptions.UserName;
            factory.Password = _brokerOptions.Password;
            factory.HostName = _brokerOptions.HostName;

            using var connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            using var channel =  connection.CreateChannelAsync().GetAwaiter().GetResult();

            channel.QueueDeclareAsync(
                queue: "DArchQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null).GetAwaiter().GetResult();

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Message: {message}");
                await channel.BasicAckAsync(ea.DeliveryTag, false);

            };

                string consumerTag = channel.BasicConsumeAsync(queue: "DArchQueue",
                autoAck: true,
                consumer: consumer).GetAwaiter().GetResult();


            Console.ReadKey();
        }
    }
}