using System.Text.Json;
using Google.Protobuf;
using Pineapple.GrpcMock.Application.Common.Converter;
using Pineapple.GrpcMock.Infrastructure.Converters.JsonConverters;

namespace Pineapple.GrpcMock.Infrastructure.Converters;

internal sealed class ProtobufConverter : IProtobufConverter
{
    private static readonly Lazy<JsonSerializerOptions> _jsonOptions = new(() =>
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        options.Converters.Add(new TimestampConverter());

        return options;
    });

    private static readonly Lazy<JsonFormatter> _jsonFormatter = new(() => new(
        new JsonFormatter.Settings(true).WithPreserveProtoFieldNames(true)));


    public IMessage FromJson(JsonElement json, Type protoType)
    {
        var proto = json.Deserialize(protoType, _jsonOptions.Value) as IMessage;
        return proto ?? throw new Exception("Can't convert json to proto");

    }

    public JsonElement ToJsonElement(IMessage proto)
    {
        var json = _jsonFormatter.Value.Format(proto);
        using JsonDocument doc = JsonDocument.Parse(json);
        return doc.RootElement.Clone();
    }

}
