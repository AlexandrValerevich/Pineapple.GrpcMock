using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.ProtoMeta.V1;

public class GetProtoMetaListApiResponse
{
    [JsonPropertyName("services")]
    public IReadOnlyList<ProtoServiceMetaApiModel> Services { get; set; } = Array.Empty<ProtoServiceMetaApiModel>();
}
