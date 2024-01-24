using ErrorOr;
using Mediator;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Proxies.Dto;

namespace Pineapple.GrpcMock.Application.Proxies.Commands.AddOrUpdateProxy;

internal sealed class AddOrUpdateProxyCommandHandler : ICommandHandler<AddOrUpdateProxyCommand, ErrorOr<Unit>>
{
    private readonly IProxyRegistry _registry;

    public AddOrUpdateProxyCommandHandler(IProxyRegistry registry)
    {
        _registry = registry;
    }

    public ValueTask<ErrorOr<Unit>> Handle(AddOrUpdateProxyCommand command, CancellationToken cancellationToken)
    {
        _registry.AddOrUpdate(new ProxyRegistryKeyDto(command.ServiceShortName), new ProxyRegistryUrlDto(command.ProxyUrl));
        return ValueTask.FromResult(ErrorOrFactory.From(Unit.Value));
    }
}
