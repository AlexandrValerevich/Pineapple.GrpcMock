using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Stubs.V1;

public class StubResponseApiModel
{
    public static readonly StubResponseApiModel Instance = new();

    [JsonPropertyName("body")]
    public JsonElement Body { get; set; }
}