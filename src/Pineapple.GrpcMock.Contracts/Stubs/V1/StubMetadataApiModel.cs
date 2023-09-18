using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Stubs.V1;

public class StubMetadataApiModel
{
    public static readonly StubMetadataApiModel Instance = new();

    [JsonExtensionData]
    public IDictionary<string, JsonElement> Trailer { get; set; } = new Dictionary<string, JsonElement>();
}
