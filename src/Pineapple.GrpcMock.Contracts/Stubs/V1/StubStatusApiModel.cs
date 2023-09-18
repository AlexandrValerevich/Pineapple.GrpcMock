using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Stubs.V1;

public class StubStatusApiModel
{
    public static readonly StubStatusApiModel Instance = new();

    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("details")]
    public string Details { get; set; } = string.Empty;
}
