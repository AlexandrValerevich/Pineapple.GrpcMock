using ErrorOr;
using Mediator;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.AddStub;

public record AddStubCommand(
    string ServiceShortName,
    string Method,
    StubStatusDto Status,
    string RequestBody,
    string ResponseBody) : ICommand<ErrorOr<Unit>>;