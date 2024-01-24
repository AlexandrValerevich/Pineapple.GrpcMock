
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pineapple.GrpcMock.Application.Common.Registry;

namespace Pineapple.GrpcMock.Infrastructure.Registry.Proxies.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection TryAddProxyRegistry(this IServiceCollection services)
    {
        services.TryAddSingleton<IProxyRegistry, ProxyRegistry>();
        return services;
    }
}
