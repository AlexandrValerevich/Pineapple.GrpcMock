using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.Stubs.V1;

public class ReadStubListApiResponse
{
    [JsonPropertyName("stubs")]
    public IReadOnlyList<StubItemApiModel> Stubs { get; set; } = Array.Empty<StubItemApiModel>();
}
