using System.Diagnostics;
using Serilog.Enrichers.Span;

namespace Pineapple.GrpcMock.RpcHost.Middlewares.TraceId;

internal sealed class TraceIdHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public TraceIdHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var traceId = Activity.Current?.GetTraceId();
        if (!string.IsNullOrEmpty(traceId))
        {
            context.Response.OnStarting(
                state =>
                {
                    ((HttpContext) state).Response.Headers.Add("x-trace-id", traceId);
                    return Task.CompletedTask;
                },
                context
            );
        }

        await _next.Invoke(context);
    }

}
