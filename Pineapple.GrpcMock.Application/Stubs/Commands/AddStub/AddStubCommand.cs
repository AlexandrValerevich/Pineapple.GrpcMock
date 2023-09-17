using ErrorOr;
using Mediator;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.AddStub;

public record AddStubCommand(
    string ServiceShortName,
    string Method,
    string RequestBody,
    string ResponseBody,
    StubStatusDto Status,
    StubMetadataDto Metadata) : ICommand<ErrorOr<Unit>>;