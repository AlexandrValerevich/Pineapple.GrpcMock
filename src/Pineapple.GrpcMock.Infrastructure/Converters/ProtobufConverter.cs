using System.Text.Json;
using Google.Protobuf;
using Pineapple.GrpcMock.Application.Common.Converter;

namespace Pineapple.GrpcMock.Infrastructure.Converters;

internal sealed class ProtobufConverter : IProtobufConverter
{
    private static readonly Lazy<JsonFormatter> _jsonFormatter = new(() => new(
        new JsonFormatter.Settings(true).WithPreserveProtoFieldNames(true)));

    public IMessage FromJson(JsonElement json, Type protoType)
    {
        if (Activator.CreateInstance(protoType) is not IMessage proto)
            throw new Exception("Can't create instance of Google.Protobuf.IMessage interface");

        IMessage result = JsonParser.Default.Parse(json.ToString(), proto.Descriptor);
        return result;
    }

    public JsonElement ToJsonElement(IMessage proto)
    {
        var json = _jsonFormatter.Value.Format(proto);
        using JsonDocument doc = JsonDocument.Parse(json);
        return doc.RootElement.Clone();
    }

}
