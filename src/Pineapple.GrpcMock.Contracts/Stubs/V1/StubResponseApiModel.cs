using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Stubs.V1;

public class StubResponseApiModel
{
    public static readonly StubResponseApiModel Instance = new();

    [JsonPropertyName("body")]
    public JsonElement Body { get; set; }

    [JsonPropertyName("status")]
    public StubStatusApiModel Status { get; set; } = StubStatusApiModel.Instance;

    [JsonPropertyName("metadata")]
    public StubMetadataApiModel Metadata { get; set; } = StubMetadataApiModel.Instance;

    [JsonPropertyName("delay")]
    public TimeSpan Delay { get; set; } = TimeSpan.Zero;
}