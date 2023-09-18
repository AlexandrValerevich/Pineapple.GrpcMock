using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.ProtoMeta.V1;

public class ProtoServiceMetaApiModel
{
    [JsonPropertyName("shortName")]
    public string ShortName { get; set; } = string.Empty;

    [JsonPropertyName("methods")]
    public IReadOnlyList<ProtoMethodMetaApiModel> Methods { get; set; } = Array.Empty<ProtoMethodMetaApiModel>();
}
