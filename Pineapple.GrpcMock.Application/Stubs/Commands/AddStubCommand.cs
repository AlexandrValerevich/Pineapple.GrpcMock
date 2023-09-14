using Mediator;

namespace Pineapple.GrpcMock.Application.Stubs.Commands;

public record AddStubCommand(
    string ServiceShortName,
    string ServiceMethod,
    string RequestBody,
    string ResponseBody
) : ICommand;