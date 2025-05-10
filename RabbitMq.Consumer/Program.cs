using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()) 
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true); 

var config = builder.Build(); 

var factory = new ConnectionFactory {HostName = config["RabbitMq:Host"]!, UserName = config["RabbitMq:Username"]!, Password = config["RabbitMq:Password"]!};
await using var connection = await factory.CreateConnectionAsync();
await using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(
    queue: "queue",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);
    
var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine("Message: {message}", message);
    
    await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(args.DeliveryTag, multiple: false);
};

await channel.BasicConsumeAsync("queue", autoAck: false, consumer);