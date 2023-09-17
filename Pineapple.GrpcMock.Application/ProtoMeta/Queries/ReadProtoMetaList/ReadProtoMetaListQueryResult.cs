using Pineapple.GrpcMock.Application.ProtoMeta.Dto;

namespace Pineapple.GrpcMock.Application.ProtoMeta.Queries.ReadProtoMetaList;

public record ReadProtoMetaListQueryResult(
   IReadOnlyList<ProtoServiceMetaDto> ServicesMeta);