using Mediator;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Proxies.Dto;

namespace Pineapple.GrpcMock.Application.Proxies.Commands.DeleteProxy;

internal sealed class DeleteProxyCommandHandler : ICommandHandler<DeleteProxyCommand>
{
    private readonly IProxyRegistry _registry;

    public DeleteProxyCommandHandler(IProxyRegistry registry)
    {
        _registry = registry;
    }

    public ValueTask<Unit> Handle(DeleteProxyCommand command, CancellationToken cancellationToken)
    {
        _registry.Delete(new ProxyRegistryKeyDto(command.ServiceShortName));
        return Unit.ValueTask;
    }
}
