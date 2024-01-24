using Mediator;

namespace Pineapple.GrpcMock.Application.Proxies.Commands.DeleteProxy;

public sealed record DeleteProxyCommand(string ServiceShortName) : ICommand;
