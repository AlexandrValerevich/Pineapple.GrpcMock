using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Stubs.V1;

public class AddStubApiRequest
{

    [JsonPropertyName("serviceShortName")]
    public string ServiceShortName { get; set; } = string.Empty;

    [JsonPropertyName("serviceMethod")]
    public string ServiceMethod { get; set; } = string.Empty;

    [JsonPropertyName("request")]
    public StubRequestApiModel Request { get; set; } = StubRequestApiModel.Instance;

    [JsonPropertyName("response")]
    public StubResponseApiModel Response { get; set; } = StubResponseApiModel.Instance;
}