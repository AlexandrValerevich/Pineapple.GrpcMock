using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Proxies.V1;

public class DeleteProxyApiRequest
{
    [JsonPropertyName("serviceShortName")]
    public string ServiceShortName { get; set; } = string.Empty;
}
