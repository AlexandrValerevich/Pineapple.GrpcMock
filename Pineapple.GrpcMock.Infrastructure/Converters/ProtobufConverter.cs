using System.Text.Json;
using Google.Protobuf;
using Pineapple.GrpcMock.Application.Common.Converter;

namespace Pineapple.GrpcMock.Infrastructure.Converters;

internal sealed class ProtobufConverter : IProtobufConverter
{
    private static readonly Lazy<JsonSerializerOptions> _jsonOptions = new(new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true
    });

    private static readonly Lazy<JsonFormatter> _jsonFormatter = new(() => new(
        new JsonFormatter.Settings(true).WithPreserveProtoFieldNames(true)));


    public IMessage FromJson(Type protoType, string json)
    {
        var proto = JsonSerializer.Deserialize(json, protoType, _jsonOptions.Value) as IMessage;
        return proto ?? throw new Exception("Can't convert json to proto");

    }

    public string ToJson(IMessage proto)
    {
        using var writer = new StringWriter();
        _jsonFormatter.Value.Format(proto, writer);
        return writer.ToString();
    }

}
