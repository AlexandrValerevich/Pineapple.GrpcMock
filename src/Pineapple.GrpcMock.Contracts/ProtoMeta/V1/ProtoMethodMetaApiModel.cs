using System.Text.Json.Serialization;

namespace Pineapple.GrpcMock.Contracts.ProtoMeta.V1;

public class ProtoMethodMetaApiModel
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;
}
