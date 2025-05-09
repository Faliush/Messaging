namespace Kafka.Consumer.Messaging.Orders;

public class OrderCreatedEvent
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
}