using Pineapple.GrpcMock.RpcHost.Configurations;
using Pineapple.GrpcMock.RpcHost.Middlewares.ServerLogging.Extensions;
using Pineapple.GrpcMock.RpcHost.Services.Interceptors;

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

        services.AddGrpc(o =>
        {
            o.Interceptors.Add<LoggingServerInterceptor>();
            o.Interceptors.Add<StubInterceptor>();
        });
        services.AddControllers();
        services.AddMinimalHttpServerLogger();
        return services;
    }
}