namespace RabbitMq.Producer.Messaging.Generic;

public interface IRabbitMqGenericProducer
{
    Task ProduceAsync<TMessage>(string routingKey, TMessage message, CancellationToken cancellationToken);
}