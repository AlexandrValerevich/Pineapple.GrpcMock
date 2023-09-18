using System.Text.Json;
using System.Text.Json.Serialization;
using Google.Protobuf.WellKnownTypes;

namespace Pineapple.GrpcMock.Infrastructure.Converters.JsonConverters;

internal sealed class TimestampConverter : JsonConverter<Timestamp>
{
    public override Timestamp Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && reader.TryGetDateTimeOffset(out DateTimeOffset dateTimeOffset))
        {
            return Timestamp.FromDateTimeOffset(dateTimeOffset);
        }

        throw new JsonException("Invalid Timestamp format");
    }

    public override void Write(Utf8JsonWriter writer, Timestamp value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToDateTimeOffset().ToString("O"));
    }
}
