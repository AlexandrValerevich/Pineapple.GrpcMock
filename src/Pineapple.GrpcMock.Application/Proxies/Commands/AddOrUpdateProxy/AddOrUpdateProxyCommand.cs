using ErrorOr;
using Mediator;

namespace Pineapple.GrpcMock.Application.Proxies.Commands.AddOrUpdateProxy;

public sealed record AddOrUpdateProxyCommand(string ServiceShortName, string ProxyUrl) : ICommand<ErrorOr<Unit>>;
