namespace Kafka.Producer.Messaging.Generic;

public interface IKafkaGenericProducer : IDisposable
{
    Task ProduceAsync<TMessage>(string topic, TMessage message, CancellationToken cancellationToken);
}