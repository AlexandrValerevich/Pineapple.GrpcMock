using Google.Protobuf;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Pineapple.GrpcMock.RpcHost.Rpc.Interceptors.Extensions;
using Pineapple.GrpcMock.RpcHost.Shared.Helpers;
using Serilog.Context;

namespace Pineapple.GrpcMock.RpcHost.Rpc.Interceptors;

internal sealed class LoggingServerInterceptor : Interceptor
{
    private readonly ILogger _logger;

    public LoggingServerInterceptor(ILogger<LoggingServerInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var timer = ValueStopwatch.StartNew();

        string? requestBody = null;
        if (_logger.IsEnabled(LogLevel.Debug) && request is IMessage requestMessage)
            requestBody = requestMessage.ToJson();

        using var requestBodyLogContext = LogContext.PushProperty("RequestBody", requestBody);
        _logger.LogTrace("Start processing handle gRPC request on server. Method: {Method}.", context.Method);

        TResponse response;
        try
        {
            response = await continuation(request, context);
        }
        catch (Exception ex)
        {
            var statusCode = ex switch
            {
                RpcException e => e.StatusCode,
                _ => context.Status.StatusCode,
            };

            _logger.LogError(ex,
                "End processing handle gRPC request on server. Method: {Method} - {StatusCode} {StatusCodeLiteral} is failed in {Elapsed:0.0000} ms.",
                context.Method, (int) statusCode, statusCode, timer.GetElapsedTime().TotalMilliseconds);

            throw;
        }

        string? responseBody = null;
        if (_logger.IsEnabled(LogLevel.Debug) && response is IMessage responseMessage)
            responseBody = responseMessage.ToJson();

        using var responseBodyLogContext = LogContext.PushProperty("ResponseBody", responseBody);
        _logger.LogInformation(
            "End processing handle gRPC request on server. Method: {Method} - {StatusCode} {StatusCodeLiteral} is complied in {Elapsed:0.0000} ms.",
            context.Method, (int) context.Status.StatusCode, context.Status.StatusCode, timer.GetElapsedTime().TotalMilliseconds);

        return response;
    }
}