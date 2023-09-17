using System.Text;
using System.Text.Json;
using Grpc.Core;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.AddStub.Extensions;

public static class MetadataExtensions
{
    public static Metadata Create(IReadOnlyDictionary<string, JsonElement> trailer)
    {
        var metadata = new Metadata();

        foreach (KeyValuePair<string, JsonElement> keyValue in trailer)
        {
            var _ = keyValue.Value.ValueKind switch
            {
                JsonValueKind.Number => metadata.TryAdd(keyValue.Key, keyValue.Value.ToString()),
                _ => metadata.TryAdd(keyValue.Key, Encoding.UTF8.GetBytes(keyValue.Value.ToString())),
            };
        }


        return metadata;
    }

    public static bool TryAdd(this Metadata metadata, string key, string value)
    {
        try
        {
            metadata.Add(key, value);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public static bool TryAdd(this Metadata metadata, string key, byte[] value)
    {
        try
        {
            metadata.Add(key + "-bin", value);
        }
        catch
        {
            return false;
        }

        return true;
    }
}
