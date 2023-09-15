using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Pineapple.GrpcMock.RpcHost.Host.Configurations;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;

namespace Pineapple.GrpcMock.RpcHost.Host.Extensions;

internal static class HostBuilderExtensions
{
    private static readonly string _executingAssembly = Assembly.GetEntryAssembly()!.GetName().Name!;

    public static IHostBuilder UseConfigurableSerilog(this IHostBuilder hostBuilder,
        Action<HostBuilderContext, LoggerConfiguration>? configure = null)
    {
        return hostBuilder.UseSerilog((host, configuration) =>
            {
                configuration.MinimumLevel.Verbose();
                configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                configuration.MinimumLevel.Override("System", LogEventLevel.Warning);
                configuration.MinimumLevel.Override("Grpc", LogEventLevel.Information);
                configuration.MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Information);
                configuration.MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information);

                configuration.Enrich.WithProperty("app", _executingAssembly);
                configuration.Enrich.FromLogContext();
                configuration.Enrich.WithSpan(new SpanOptions
                {
                    IncludeTags = true,
                    IncludeBaggage = true
                });

                configuration.ReadFrom.Configuration(host.Configuration);
                configure?.Invoke(host, configuration);
            }
        );
    }

    public static IWebHostBuilder ConfigureKestrel(this IWebHostBuilder builder)
    {
        return builder.UseKestrel(options =>
        {
            var configuration = options.ApplicationServices.GetRequiredService<IConfiguration>();
            KestrelListenerOptions? listenerOptions = configuration.GetValue<KestrelListenerOptions>("KestrelListeners");

            options.Listen(IPAddress.Any, listenerOptions?.Http1.Port ?? 5001, options =>
            {
                options.Protocols = HttpProtocols.Http1;
            });

            options.Listen(IPAddress.Any, listenerOptions?.Http2.Port ?? 5002, options =>
            {
                options.Protocols = HttpProtocols.Http2;
            });
        });
    }

}