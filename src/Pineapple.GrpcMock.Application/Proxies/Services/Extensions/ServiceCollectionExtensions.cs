using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Pineapple.GrpcMock.Application.Proxies.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection TryAddProxyService(this IServiceCollection services)
    {
        services.TryAddSingleton<IProxyService, ProxyService>();
        return services;
    }
}
