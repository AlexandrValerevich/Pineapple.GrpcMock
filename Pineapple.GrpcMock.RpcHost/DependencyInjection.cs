using Pineapple.GrpcMock.RpcHost.Configurations;
using Pineapple.GrpcMock.RpcHost.Middlewares.ServerLogging.Extensions;

namespace Pineapple.GrpcMock.RpcHost;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services,
        Action<StubOptions> stubOptions)
    {
        services.AddOptions<StubOptions>()
            .Configure(stubOptions)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddMinimalHttpServerLogger();
        services.AddControllers();
        return services;
    }
}
