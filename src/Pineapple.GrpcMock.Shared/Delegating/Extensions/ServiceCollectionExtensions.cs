using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;

namespace Pineapple.GrpcMock.Shared.Delegating.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection ReplaceHttpClientLogger(this IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, HttpClientLoggerMessageHandlerBuilderFilter>());
        return services;
    }
}