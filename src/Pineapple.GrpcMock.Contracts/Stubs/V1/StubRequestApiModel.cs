using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Stubs.V1;

public class StubRequestApiModel
{
    public static readonly StubRequestApiModel Instance = new();

    [JsonPropertyName("body")]
    public JsonElement Body { get; set; }
}