using Kafka.Consumer.Messaging.Orders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Consumer.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaOrdersSettings>(configuration.GetSection("Kafka:Orders"));
        services.AddHostedService<OrdersConsumer>();
        services.AddSingleton<OrderCreatedMessageHandler>();

        return services;
    }
}