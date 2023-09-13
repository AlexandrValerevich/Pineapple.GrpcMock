using Microsoft.Extensions.Options;
using Pineapple.GrpcMock.RpcHost.Logging.Configurations;

namespace LigaStavok.Feed.BaseLine.Api.Middleware.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMinimalHttpServerLogger(this IServiceCollection services)
    {
        var settings = new ServerHttpLoggerOptions();
        services.AddMinimalHttpServerLogger(settings);
        return services;
    }

    public static IServiceCollection AddMinimalHttpServerLogger(this IServiceCollection services,
        ServerHttpLoggerOptions settings)
    {
        services.AddSingleton(Options.Create(settings));
        return services;
    }

    public static IServiceCollection AddMinimalHttpServerLogger(this IServiceCollection services,
        Action<ServerHttpLoggerOptions> configure)
    {
        services.AddOptions<ServerHttpLoggerOptions>()
            .Configure(configure);
        return services;
    }
}