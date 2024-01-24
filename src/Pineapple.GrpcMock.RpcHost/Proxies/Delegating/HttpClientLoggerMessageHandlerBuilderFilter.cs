
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Http.Logging;

namespace Pineapple.GrpcMock.RpcHost.Proxies.Delegating;

internal sealed class HttpClientLoggerMessageHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
{
    private readonly ILogger<HttpClientLoggerDelegatingHandler> _logger;

    public HttpClientLoggerMessageHandlerBuilderFilter(ILogger<HttpClientLoggerDelegatingHandler> logger)
    {
        _logger = logger;
    }

    public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
    {
        return builder =>
        {
            next(builder);

            List<DelegatingHandler> toRemove = builder.AdditionalHandlers
                .Where(h => h is LoggingHttpMessageHandler || h is LoggingScopeHttpMessageHandler)
                .ToList();

            foreach (DelegatingHandler delegatingHandler in toRemove)
            {
                builder.AdditionalHandlers.Remove(delegatingHandler);
            }

            builder.AdditionalHandlers.Add(new HttpClientLoggerDelegatingHandler(_logger));
        };
    }
}