using Pineapple.GrpcMock.Application.ProtoMeta.Dto;

namespace Pineapple.GrpcMock.Application.Common.Registry;

public interface IProtoMetaRegistry
{
    IReadOnlyList<ProtoMetaDto> List();
    ProtoMetaDto? Get(string shortName);
}