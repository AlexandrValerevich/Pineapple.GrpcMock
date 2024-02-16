using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pineapple.GrpcMock.Application.Proxies.Services;
using Pineapple.GrpcMock.Shared.Interceptors.Extensions;
using Pineapple.GrpcMock.Shared.ServiceCollection;

namespace Pineapple.GrpcMock.Application.Proxies.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection TryAddProxyService(this IServiceCollection services)
    {
        if (!services.Contain<IProxyService>())
        {
            services.TryAddSingleton<IProxyService, ProxyService>();
            services.Decorate<IProxyService, ProxyServiceLoggerDecorator>();
            services.TryAddLoggingClientInterceptorFactory();
        }
        return services;
    }
}
