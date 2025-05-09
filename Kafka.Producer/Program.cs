using Kafka.Producer.Messaging;
using Kafka.Producer.Messaging.Orders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddKafka(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("orders/create", async (IOrderCreatedProducer producer, CancellationToken token) =>
{
    await producer.ProduceAsync(new OrderCreatedEvent(Guid.NewGuid(), "empty"), token);
});

app.Run();