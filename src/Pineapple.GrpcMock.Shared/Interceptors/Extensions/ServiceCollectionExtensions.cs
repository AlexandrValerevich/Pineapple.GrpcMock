using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Pineapple.GrpcMock.Shared.Interceptors.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection TryAddLoggingClientInterceptorFactory(this IServiceCollection services)
    {
        services.TryAddSingleton<ILoggingClientInterceptorFactory, LoggingClientInterceptorFactory>();
        return services;
    }
}
