using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace Kafka.Producer.Messaging.Generic;

public class KafkaGenericProducer : IKafkaGenericProducer
{
    private readonly IProducer<string, string> _producer;

    public KafkaGenericProducer(IConfiguration configuration)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:Server"]
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task ProduceAsync<TMessage>(string topic, TMessage message, CancellationToken cancellationToken)
    {
        var value = JsonSerializer.Serialize(message);

        await _producer.ProduceAsync(topic, new Message<string, string>()
        {
            Key = Guid.NewGuid().ToString(),
            Value = value
        }, cancellationToken);
    }

    public void Dispose() => _producer.Dispose();
}