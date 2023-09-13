using System.Reflection;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;

namespace Pineapple.GrpcMock.RpcHost.Extensions;

internal static class HostBuilderExtensions
{
    private static readonly string _executingAssembly = Assembly.GetEntryAssembly()!.GetName().Name!;

    public static IHostBuilder UseConfigurableSerilog(
        this IHostBuilder hostBuilder,
        Action<HostBuilderContext, LoggerConfiguration>? configure = null)
    {
        return hostBuilder
            .UseSerilog((hostBuilderContext, configuration) =>
                {
                    configuration.MinimumLevel.Verbose();
                    configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                    configuration.MinimumLevel.Override("System", LogEventLevel.Warning);
                    configuration.MinimumLevel.Override("Grpc", LogEventLevel.Information);
                    configuration.MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Information);
                    configuration.MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information);

                    configuration.Enrich.FromLogContext();
                    configuration.Enrich.WithSpan(new SpanOptions
                    {
                        IncludeTags = true,
                        IncludeBaggage = true
                    });

                    configuration.Enrich.WithProperty("app", _executingAssembly);
                    configuration.ReadFrom.Configuration(hostBuilderContext.Configuration);
                    configure?.Invoke(hostBuilderContext, configuration);
                }
            );
    }

}
