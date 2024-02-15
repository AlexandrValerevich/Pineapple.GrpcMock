using Microsoft.Extensions.Logging;
using Pineapple.GrpcMock.RpcHost.Shared.Helpers;
using Serilog.Context;

namespace Pineapple.GrpcMock.Shared.Delegating;

internal sealed class HttpClientLoggerDelegatingHandler : DelegatingHandler
{
    private readonly ILogger _logger;
    private const int LengthLimit = 32 * 1024;

    public HttpClientLoggerDelegatingHandler(ILogger<HttpClientLoggerDelegatingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var timer = ValueStopwatch.StartNew();

        string? requestBody = null;
        if (_logger.IsEnabled(LogLevel.Debug))
            requestBody = await ReadBodyAsync(request.Content, cancellationToken);

        using var requestBodyLogContext = LogContext.PushProperty("RequestBody", requestBody);

        var requestUri = request.RequestUri?.ToString();
        _logger.LogTrace("Start processing Http/{HttpVersion} {Method} {Uri}",
            request.Version, request.Method, requestUri);

        HttpResponseMessage response;
        try
        {
            response = await base.SendAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "End processing Http/{HttpVersion} {Method} {Uri} in {Elapsed:0.0000} ms. Process result: [Failed]",
                request.Version, request.Method, request.RequestUri?.ToString(), timer.GetElapsedTime().TotalMilliseconds);

            throw;
        }

        string? responseBody = null;
        if (_logger.IsEnabled(LogLevel.Debug))
            responseBody = await ReadBodyAsync(response.Content, cancellationToken);

        using var responseBodyLogContext = LogContext.PushProperty("ResponseBody", responseBody);

        if (response.IsSuccessStatusCode)
            _logger.LogInformation("End processing Http/{HttpVersion} {Method} {Uri} - {StatusCode} {StatusCodeLiteral} in {Elapsed:0.0000} ms. Process result: [Successful]", request.Version, request.Method, requestUri, (int) response.StatusCode, response.StatusCode, timer.GetElapsedTime().TotalMilliseconds);
        else
        {
            _logger.LogInformation("End processing Http/{HttpVersion} {Method} {Uri} - {StatusCode} {StatusCodeLiteral} in {Elapsed:0.0000} ms. Process result: [Failed]", request.Version, request.Method, requestUri, (int) response.StatusCode, response.StatusCode, timer.GetElapsedTime().TotalMilliseconds);
        }


        return response;
    }

    private static async Task<string> ReadBodyAsync(HttpContent? content, CancellationToken cancellationToken)
    {
        if (content == null)
            return string.Empty;

        var body = await content.ReadAsStringAsync(cancellationToken);
        return TruncateIfTooLong(body);
    }

    private static string TruncateIfTooLong(string body)
    {
        if (body.Length > LengthLimit)
        {
            var spanJson = body.AsSpan();
            return spanJson[..LengthLimit].ToString();
        }

        return body;
    }
}