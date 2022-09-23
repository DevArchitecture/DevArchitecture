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
            var factory = new ConnectionFactory()
            {
                HostName = _brokerOptions.HostName,
                UserName = _brokerOptions.UserName,
                Password = _brokerOptions.Password
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: "DArchQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, mq) =>
            {
                var body = mq.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message: {message}");
            };

            channel.BasicConsume(
                queue: "DArchQueue",
                autoAck: true,
                consumer: consumer);
            Console.ReadKey();
        }
    }
}