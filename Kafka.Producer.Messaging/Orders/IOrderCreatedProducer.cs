namespace Kafka.Producer.Messaging.Orders;

public interface IOrderCreatedProducer : IKafkaProducer<OrderCreatedEvent>;