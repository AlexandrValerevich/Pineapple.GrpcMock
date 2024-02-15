using Pineapple.GrpcMock.Application.ProtoMeta.Dto;

namespace Pineapple.GrpcMock.Application.Common.Registry;

public interface IProtoMetaRegistry
{
    IReadOnlyList<ProtoServiceMetaDto> List();
    ProtoServiceMetaDto? Get(string serviceShortName);
}