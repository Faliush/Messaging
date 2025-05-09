using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Kafka.Producer.Messaging.Orders;

internal sealed class OrderCreatedProducer : IOrderCreatedProducer
{
    private readonly IProducer<string, OrderCreatedEvent> _producer;
    private readonly string _topic;
    
    public OrderCreatedProducer(IOptions<KafkaOrdersSettings> options)
    {
        _topic = options.Value.OrderCreatedTopic;
        
        var config = new ProducerConfig
        {
            BootstrapServers = options.Value.Server
        };

        _producer = new ProducerBuilder<string, OrderCreatedEvent>(config)
            .SetValueSerializer(new KafkaJsonSerializer<OrderCreatedEvent>())
            .Build();
    }

    public async Task ProduceAsync(OrderCreatedEvent message, CancellationToken cancellationToken)
    {
        await _producer.ProduceAsync(_topic, new Message<string, OrderCreatedEvent>()
        {
            Key = Guid.NewGuid().ToString(),
            Value = message
        }, cancellationToken);
    }

    public void Dispose() => _producer.Dispose();
    
}