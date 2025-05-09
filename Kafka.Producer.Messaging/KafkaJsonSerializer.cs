using System.Text.Json;
using Confluent.Kafka;

namespace Kafka.Producer.Messaging;

public class KafkaJsonSerializer<TMessage> : ISerializer<TMessage>
{
    public byte[] Serialize(TMessage data, SerializationContext context) => JsonSerializer.SerializeToUtf8Bytes(data);
}