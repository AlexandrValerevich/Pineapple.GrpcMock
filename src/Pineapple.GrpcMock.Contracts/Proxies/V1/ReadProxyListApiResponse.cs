using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Proxies.V1;

public class ReadProxyListApiResponse
{
    [JsonPropertyName("proxies")]
    public IReadOnlyList<ProxyItemApiModel> Proxies { get; set; } = Array.Empty<ProxyItemApiModel>();
}
