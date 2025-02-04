using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.Utilities.MessageBrokers.RabbitMq;

public class MqConsumerHelper : IMessageConsumer
{
    private readonly MessageBrokerOptions _brokerOptions;

    public MqConsumerHelper(IConfiguration configuration)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        _brokerOptions = configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
        if (_brokerOptions == null)
            throw new InvalidOperationException("MessageBrokerOptions configuration section is missing.");
    }

    public async Task GetQueue()
    {

        using var connection = await new ConnectionFactory
        {
            HostName = _brokerOptions.HostName,
            Port = _brokerOptions.Port,
            UserName = _brokerOptions.UserName,
            Password = _brokerOptions.Password,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(2)
        }.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            const string queueName = "DArchQueue";

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, mq) =>
            {
                try
                {
                    var body = mq.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Message received: {message}");

                    await channel.BasicAckAsync(mq.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            };

            await channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: false,
                consumer: consumer);
            Console.ReadLine();
    }
}