using System.Text;
using System.Text.Json;
using Grpc.Core;

namespace Pineapple.GrpcMock.Application.Common.Extensions;

public static class MetadataExtensions
{
    public static Metadata Create(IReadOnlyDictionary<string, JsonElement> trailer)
    {
        var metadata = new Metadata();

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
}
