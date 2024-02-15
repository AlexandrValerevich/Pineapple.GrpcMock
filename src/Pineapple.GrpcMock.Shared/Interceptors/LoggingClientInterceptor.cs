using Google.Protobuf;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using Pineapple.GrpcMock.RpcHost.Shared.Helpers;
using Serilog.Context;

namespace Pineapple.GrpcMock.Shared.Interceptors;
internal sealed class LoggingClientInterceptor : Interceptor
{
    private readonly ILogger _logger;

    public LoggingClientInterceptor(ILogger<LoggingClientInterceptor> logger)
    {
        _logger = logger;
    }

    public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation) where TRequest : class where TResponse : class
    {
        var timer = ValueStopwatch.StartNew();

        string? requestBody = null;
        if (_logger.IsEnabled(LogLevel.Debug) && request is IMessage requestMessage)
            requestBody = requestMessage.ToString();

        using var requestBodyLogContext = LogContext.PushProperty("RequestBody", requestBody);
        _logger.LogTrace("Start processing handle gRPC request on client. Method: {Method}.", context.Method);

        TResponse response;
        Status status;
        try
        {
            response = continuation(request, context);
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex,
                "End processing handle gRPC request on client. Method: {Method} - {StatusCode} {StatusCodeLiteral} in {Elapsed:0.0000} ms. Process result: Failed.",
                context.Method, (int) ex.StatusCode, ex.StatusCode, timer.GetElapsedTime().TotalMilliseconds);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "End processing handle gRPC request on client. Method: {Method} in {Elapsed:0.0000} ms. Process result: Failed.",
                context.Method, timer.GetElapsedTime().TotalMilliseconds);

            throw;
        }

        status = Status.DefaultSuccess;
        string? responseBody = null;
        if (_logger.IsEnabled(LogLevel.Debug) && response is IMessage responseMessage)
            responseBody = responseMessage.ToString();

        using var responseBodyLogContext = LogContext.PushProperty("ResponseBody", responseBody);

        _logger.LogInformation(
           "End processing handle gRPC request on client. Method: {Method} - {StatusCode} {StatusCodeLiteral} in {Elapsed:0.0000} ms. Process result: Successful.",
           context.Method, (int) status.StatusCode, status.StatusCode, timer.GetElapsedTime().TotalMilliseconds);

        return response;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        AsyncUnaryCall<TResponse> call = continuation(request, context);
        return new AsyncUnaryCall<TResponse>(
           HandleResponseWithLogging(call.ResponseAsync, call.GetStatus, request, context.Method.FullName),
           call.ResponseHeadersAsync,
           call.GetStatus,
           call.GetTrailers,
           call.Dispose);
    }

    private async Task<TResponse> HandleResponseWithLogging<TRequest, TResponse>(Task<TResponse> inner, Func<Status> statusCallback, TRequest request, string method)
    {
        var timer = ValueStopwatch.StartNew();

        string? requestBody = null;
        if (_logger.IsEnabled(LogLevel.Debug) && request is IMessage requestMessage)
            requestBody = requestMessage.ToString();

        using var requestBodyLogContext = LogContext.PushProperty("RequestBody", requestBody);
        _logger.LogTrace("Start processing handle gRPC request on client. Method: {Method}.", method);

        TResponse response;
        Status status;
        try
        {
            response = await inner;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex,
                "End processing handle gRPC request on client. Method: {Method} - {StatusCode} {StatusCodeLiteral} in {Elapsed:0.0000} ms. Process result: Failed.",
                method, (int) ex.StatusCode, ex.StatusCode, timer.GetElapsedTime().TotalMilliseconds);

            throw;
        }
        catch (Exception ex)
        {
            status = statusCallback();
            _logger.LogError(ex,
                "End processing handle gRPC request on client. Method: {Method} - {StatusCode} {StatusCodeLiteral} in {Elapsed:0.0000} ms. Process result: Failed.",
                method, (int) status.StatusCode, status.StatusCode, timer.GetElapsedTime().TotalMilliseconds);

            throw;
        }

        status = statusCallback();
        string? responseBody = null;
        if (_logger.IsEnabled(LogLevel.Debug) && response is IMessage responseMessage)
            responseBody = responseMessage.ToString();

        using var responseBodyLogContext = LogContext.PushProperty("ResponseBody", responseBody);

        _logger.LogInformation(
           "End processing handle gRPC request on client. Method: {Method} - {StatusCode} {StatusCodeLiteral} in {Elapsed:0.0000} ms. Process result: Successful.",
           method, (int) status.StatusCode, status.StatusCode, timer.GetElapsedTime().TotalMilliseconds);

        return response;
    }
}
