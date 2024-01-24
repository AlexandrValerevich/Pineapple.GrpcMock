using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;

namespace Pineapple.GrpcMock.RpcHost.Proxies.Delegating.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ReplaceHttpClientLogger(this IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, HttpClientLoggerMessageHandlerBuilderFilter>());
        return services;
    }
}