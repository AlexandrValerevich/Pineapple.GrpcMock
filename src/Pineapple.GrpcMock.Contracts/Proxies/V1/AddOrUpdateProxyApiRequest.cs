using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Proxies.V1;

public class AddOrUpdateProxyApiRequest
{
    [JsonPropertyName("serviceShortName")]
    public string ServiceShortName { get; set; } = string.Empty;

    [JsonPropertyName("proxyTo")]
    public string ProxyTo { get; set; } = string.Empty;
}
