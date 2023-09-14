using Mediator;

namespace Pineapple.GrpcMock.Application.Stubs.Commands;

public record AddStubCommand(
    string ServiceName,
    string ServiceMethod,
    byte[] RequestBody
) : ICommand;