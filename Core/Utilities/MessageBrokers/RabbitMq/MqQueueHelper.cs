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

    public Task<IResult> QueueMessageAsync<T>(T messageModel)
    {
        using var connection = new ConnectionFactory()
            {
                HostName = _brokerOptions.HostName,
                Port = _brokerOptions.Port,
                UserName = _brokerOptions.UserName,
                Password = _brokerOptions.Password,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = new TimeSpan(2000),
            }
            .CreateConnection();
        using var channel = connection.CreateModel();
        var topicName = typeof(T).Name;
        channel.QueueDeclare(
            topicName,
            false,
            false,
            false,
            null);

        var message = JsonConvert.SerializeObject(messageModel);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(string.Empty, topicName, null, body);
        return Task.FromResult<IResult>(new SuccessResult());

    }
}