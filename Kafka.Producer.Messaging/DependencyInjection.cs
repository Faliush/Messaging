using Kafka.Producer.Messaging.Orders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Producer.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaOrdersSettings>(configuration.GetSection("Kafka:Orders"));
        services.AddSingleton<IOrderCreatedProducer, OrderCreatedProducer>();

        return services;
    }
}