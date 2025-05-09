using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace RabbitMq.Producer.Messaging.Generic;

public sealed class RabbitMqGenericProducer : IRabbitMqGenericProducer
{
    private readonly RabbitMqSettings _settings;
    private readonly ConnectionFactory _factory;
    
    public RabbitMqGenericProducer(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;
        _factory = new ConnectionFactory { HostName = _settings.Host, UserName = _settings.Username, Password = _settings.Password };
    }
    
    public async Task ProduceAsync<TMessage>(string routingKey, TMessage message, CancellationToken cancellationToken)
    {
        await using var connection = await _factory.CreateConnectionAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: routingKey, 
            durable: true, 
            exclusive: false, 
            autoDelete: false,
            arguments: null, 
            cancellationToken: cancellationToken);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: routingKey,
            mandatory: true,
            basicProperties: new BasicProperties { Persistent = true },
            body: body,
            cancellationToken: cancellationToken);
    }
}