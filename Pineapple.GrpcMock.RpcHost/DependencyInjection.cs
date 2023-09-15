using Pineapple.GrpcMock.RpcHost.Configurations;
using Pineapple.GrpcMock.RpcHost.Middlewares.ServerLogging.Extensions;
using Pineapple.GrpcMock.RpcHost.OutputFormatters;

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

        services.AddGrpc();
        services.AddMinimalHttpServerLogger();
        services.AddControllers()
            .AddMvcOptions(o =>
            {
                o.OutputFormatters.Add(new GrpcOutputFormatter());
            });
        return services;
    }
}
