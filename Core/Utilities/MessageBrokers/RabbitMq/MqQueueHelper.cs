using System;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Core.Utilities.MessageBrokers.RabbitMq;

public class RMqQueueHelper : IMessageBrokerHelper
{
    private readonly MessageBrokerOptions _brokerOptions;

    public RMqQueueHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _brokerOptions = Configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
    }

    public IConfiguration Configuration { get; }

    public async Task<IResult> QueueMessageAsync<T>(T messageModel)
    {
        var factory = new ConnectionFactory
            {
                HostName = _brokerOptions.HostName,
                Port = _brokerOptions.Port,
                UserName = _brokerOptions.UserName,
                Password = _brokerOptions.Password,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(2)
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
        var topicName = typeof(T).Name;
        await channel.QueueDeclareAsync(
            topicName,
            false,
            false,
            false,
            null);

        var message = JsonConvert.SerializeObject(messageModel);
        var body = Encoding.UTF8.GetBytes(message);

        var props = new BasicProperties();

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: topicName,
            mandatory: false,
            basicProperties: props,
            body: body);

        return new SuccessResult();
    }
}