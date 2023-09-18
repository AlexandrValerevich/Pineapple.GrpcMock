using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubList;

public record ReadStubListQueryResult(
    IReadOnlyList<StubItemDto> Stubs
);
