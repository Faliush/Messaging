using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Kafka.Consumer.Messaging.Orders;

public class OrdersConsumer : BackgroundService
{
    private readonly string _topic;
    private readonly OrderCreatedMessageHandler _handler;
    private readonly IConsumer<string, OrderCreatedEvent> _consumer;

    public OrdersConsumer(IOptions<KafkaOrdersSettings> options, OrderCreatedMessageHandler handler)
    {
        _topic = options.Value.OrderCreatedTopic;
        _handler = handler;
        
        var config = new ConsumerConfig
        {
            BootstrapServers = options.Value.Server,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            GroupId = options.Value.GroupId
        };

        _consumer = new ConsumerBuilder<string, OrderCreatedEvent>(config)
            .SetValueDeserializer(new KafkaJsonDeserializer<OrderCreatedEvent>())
            .Build();
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
        => Task.Run(() => ConsumeAsync(stoppingToken), stoppingToken);

    private async Task? ConsumeAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(_topic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(stoppingToken);
                await _handler.HandleAsync(result.Message.Value, stoppingToken);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        return base.StopAsync(cancellationToken);
    }
}