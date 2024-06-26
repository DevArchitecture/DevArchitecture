
using System.Threading.Tasks;
using Confluent.Kafka;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Utilities.MessageBrokers.Kafka;

public class KafkaMessageBroker : IMessageBrokerHelper
{
    private readonly MessageBrokerOptions _kafkaOptions;

    public KafkaMessageBroker()
    {
        var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        if (configuration != null)
            _kafkaOptions = configuration
                .GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
    }

    public async Task<IResult> QueueMessageAsync<T>(T messageModel)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = $"{_kafkaOptions.HostName}:{_kafkaOptions.Port}",
            Acks = Acks.All
        };

        var message = JsonConvert.SerializeObject(messageModel);
        var topicName = typeof(T).Name;
        using var p = new ProducerBuilder<Null, string>(producerConfig).Build();
        try
        {
            await p.ProduceAsync(topicName
                , new Message<Null, string>
                {
                    Value = message
                });
            return new SuccessResult();
        }

        catch (ProduceException<Null, string> e)
        {
            return new ErrorResult(e.Message);
        }
    }
}