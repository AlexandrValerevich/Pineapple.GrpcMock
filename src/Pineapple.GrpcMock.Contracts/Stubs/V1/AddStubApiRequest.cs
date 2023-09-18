using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Stubs.V1;

public class AddStubApiRequest
{

    [JsonPropertyName("serviceShortName")]
    public string ServiceShortName { get; set; } = string.Empty;

    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;

    [JsonPropertyName("priority")]
    public int Priority { get; set; } = 0;

    [JsonPropertyName("request")]
    public StubRequestApiModel Request { get; set; } = StubRequestApiModel.Instance;

    [JsonPropertyName("response")]
    public StubResponseApiModel Response { get; set; } = StubResponseApiModel.Instance;
}