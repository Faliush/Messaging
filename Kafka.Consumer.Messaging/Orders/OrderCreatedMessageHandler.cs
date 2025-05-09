using Microsoft.Extensions.Logging;

namespace Kafka.Consumer.Messaging.Orders;

public class OrderCreatedMessageHandler(ILogger<OrderCreatedMessageHandler> logger) : IMessageHandler<OrderCreatedEvent>
{
    public Task HandleAsync(OrderCreatedEvent message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Message: {message}", message);
        return Task.CompletedTask;
    }
}