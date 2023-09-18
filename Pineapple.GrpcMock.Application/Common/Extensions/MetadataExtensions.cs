using System.Text;
using System.Text.Json;
using Grpc.Core;

namespace Pineapple.GrpcMock.Application.Common.Extensions;

public static class MetadataExtensions
{
    public static Metadata Add(this Metadata metadata, IReadOnlyDictionary<string, JsonElement> trailer)
    {
        foreach (KeyValuePair<string, JsonElement> keyValue in trailer)
        {
            if (keyValue.Key.EndsWith("-bin"))
            {

                metadata.TryAdd(keyValue.Key, Encoding.UTF8.GetBytes(keyValue.Value.ToString()));
            }
            else
            {
                metadata.TryAdd(keyValue.Key, keyValue.Value.ToString());
            }
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
            metadata.Add(key, value);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public static IReadOnlyDictionary<string, JsonElement> ToReadOnlyDictionary(this Metadata metadata)
    {
        var result = new Dictionary<string, JsonElement>();
        foreach (Metadata.Entry metadataItem in metadata)
        {
            result.Add(
                metadataItem.Key,
                JsonSerializer.SerializeToElement(metadataItem.IsBinary ? Encoding.UTF8.GetString(metadataItem.ValueBytes) : metadataItem.Value));
        }

        return result;
    }
}
