using Mediator;

namespace Pineapple.GrpcMock.Application.Stubs.Commands;

public record AddStubCommand(
    string ServiceShortName,
    string Method,
    string RequestBody,
    string ResponseBody) : ICommand;