using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Stubs.V1;

public class RemoveStubListApiRequest
{
    [JsonPropertyName("serviceShortName")]
    public string ServiceShortName { get; set; } = string.Empty;

    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;
}
