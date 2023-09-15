using Google.Protobuf;

namespace Pineapple.GrpcMock.RpcHost.Services.Interceptors.Extensions;

public static class ProtobufMessageExtensions
{
    private static readonly Lazy<JsonFormatter> _jsonFormatter = new(() => new(new JsonFormatter.Settings(true).WithPreserveProtoFieldNames(true)));
    private const int LengthLimit = 32 * 1024;

    public static string ToJson(this IMessage protobufMessage, int lengthLimit = LengthLimit)
    {
        var body = SerializeToJson(protobufMessage);
        return TruncateIfTooLong(body, lengthLimit);
    }

    private static string SerializeToJson(IMessage protobufMessage)
    {
        using var writer = new StringWriter();
        _jsonFormatter.Value.Format(protobufMessage, writer);
        return writer.ToString();
    }

    private static string TruncateIfTooLong(string body, int lengthLimit)
    {
        if (body.Length > lengthLimit)
        {
            var spanJson = body.AsSpan();
            return spanJson[..lengthLimit].ToString();
        }

        return body;
    }
}