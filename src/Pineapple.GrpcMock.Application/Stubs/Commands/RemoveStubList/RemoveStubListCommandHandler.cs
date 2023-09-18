using ErrorOr;
using Mediator;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.RemoveStubList;

internal sealed class RemoveStubListCommandHandler : ICommandHandler<RemoveStubListCommand, ErrorOr<Unit>>
{
    private readonly IStubRegistry _registry;

    public RemoveStubListCommandHandler(IStubRegistry registry)
    {
        _registry = registry;
    }

    public ValueTask<ErrorOr<Unit>> Handle(RemoveStubListCommand command, CancellationToken cancellationToken)
    {
        _registry.Remove(new StubRegistryKeyDto(
            ServiceShortName: command.ServiceShortName,
            Method: command.Method));

        return ValueTask.FromResult(ErrorOrFactory.From(Unit.Value));
    }
}
