namespace Kafka.Producer.Messaging.Orders;

public record OrderCreatedEvent(Guid Id, string Type);