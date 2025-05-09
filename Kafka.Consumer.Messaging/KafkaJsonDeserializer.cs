using System.Text.Json;
using Confluent.Kafka;

namespace Kafka.Consumer.Messaging;

public class KafkaJsonDeserializer<TMessage> : IDeserializer<TMessage>
{
    public TMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        => JsonSerializer.Deserialize<TMessage>(data)!;
}