using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMq.Producer.Messaging.Generic;

namespace RabbitMq.Producer.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));
        services.AddScoped<IRabbitMqGenericProducer, RabbitMqGenericProducer>();

        return services;
    }
}