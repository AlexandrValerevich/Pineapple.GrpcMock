using ErrorOr;
using Mediator;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.AddStub;

public record AddStubCommand(
    string ServiceShortName,
    string Method,
    string RequestBody,
    string ResponseBody) : ICommand<ErrorOr<Unit>>;