namespace Kafka.Producer.Messaging;

public interface IKafkaProducer<in TMessage> : IDisposable
{
    Task ProduceAsync(TMessage message, CancellationToken cancellationToken);
}