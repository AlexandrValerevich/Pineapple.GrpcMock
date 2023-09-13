using System.Net;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Pineapple.GrpcMock.RpcHost.Logging.Configurations;
using Pineapple.GrpcMock.RpcHost.Logging.Helpers;
using Serilog;
using Serilog.Events;

namespace Pineapple.GrpcMock.RpcHost.Logging;

internal sealed class HttpServerLoggerMiddleware
{
    private const string BodyOutOfLimit = "[Body length is out of limit]";

    private readonly RequestDelegate _next;
    private readonly ServerHttpLoggerOptions _settings;

    public HttpServerLoggerMiddleware(RequestDelegate next, IOptions<ServerHttpLoggerOptions> options)
    {
        _next = next;
        _settings = options.Value;
    }

    public Task InvokeAsync(HttpContext context)
    {
        if (!Log.Logger.IsEnabled(LogEventLevel.Information))
            return _next(context);

        return InvokeInternalAsync(context);
    }

    private async Task InvokeInternalAsync(HttpContext context)
    {
        var logger = Log.Logger;
        if (logger.IsEnabled(LogEventLevel.Debug))
        {
            var requestBody = await GetRequestBody(context.Request);
            var requestHeaders = GetHeaders(context.Request.Headers);

            logger = logger.ForContext("RequestBody", requestBody)
                .ForContext("RequestHeaders", requestHeaders);
        }

        logger.Verbose("Start processing {Protocol} {Method} {Path}{QueryString}",
            context.Request.Protocol,
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString);

        var stopwatch = ValueStopwatch.StartNew();
        if (logger.IsEnabled(LogEventLevel.Debug))
        {
            var responseBody = await GetResponseBody(context);
            var responseHeaders = GetHeaders(context.Response.Headers);
            logger = logger.ForContext("ResponseBody", responseBody)
                .ForContext("ResponseHeaders", responseHeaders);
        }
        else
        {
            await _next(context);
        }

        logger.Information("End processing {Protocol} {Method} {Path}{QueryString} - {StatusCode} {StatusCodeLiteral} in {Elapsed:0.0000} ms",
            context.Request.Protocol,
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString,
            context.Response.StatusCode,
            (HttpStatusCode) context.Response.StatusCode,
            stopwatch.GetElapsedTime().TotalMilliseconds);
    }

    private async Task<string> GetRequestBody(HttpRequest request)
    {
        if (!request.ContentLength.HasValue || request.Body == null)
            return string.Empty;

        if (request.ContentLength > _settings.RequestBodyLogLimit)
            return BodyOutOfLimit;

        request.EnableBuffering();
        var buffer = new byte[request.ContentLength.Value];
        await request.Body.ReadAsync(buffer);
        var requestBody = Encoding.UTF8.GetString(buffer);
        request.Body.Seek(0, SeekOrigin.Begin);

        return requestBody;
    }

    private async Task<string> GetResponseBody(HttpContext context)
    {
        var responseBody = string.Empty;
        if (context.Response.Body == null)
            return responseBody;

        var originalBody = context.Response.Body;
        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        await _next(context);

        memoryStream.Position = 0;
        if (memoryStream.Length > _settings.ResponseBodyLogLimit)
            return BodyOutOfLimit;
        else if (memoryStream.Length > 0)
        {
            responseBody = new StreamReader(memoryStream).ReadToEnd();
            memoryStream.Position = 0;
        }

        await memoryStream.CopyToAsync(originalBody);
        context.Response.Body = originalBody;
        return responseBody;
    }

    private Dictionary<string, string> GetHeaders(IHeaderDictionary headersDictionary)
    {
        var loggedHeaders = new Dictionary<string, string>();
        foreach ((string key, StringValues value) in headersDictionary)
        {
            loggedHeaders[key] = _settings.ResponseHeaders.Contains(key) ? value.ToString() : "[Redacted]";
        }

        string headers = string.Join(",", loggedHeaders.Select(x => $"\"{x.Key}\":\"{x.Value}\""));
        return loggedHeaders;
    }
}
