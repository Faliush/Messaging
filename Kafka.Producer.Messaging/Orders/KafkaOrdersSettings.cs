namespace Kafka.Producer.Messaging.Orders;

public class KafkaOrdersSettings
{
    public string Server { get; set; } = string.Empty;
    public string OrderCreatedTopic { get; set; } = string.Empty;
}