using Grpc.Core;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using Pineapple.GrpcMock.Application.Proxies.Services.Dto;
using Pineapple.GrpcMock.RpcHost.Shared.Helpers;

namespace Pineapple.GrpcMock.Application.Proxies.Services;

internal sealed class ProxyServiceLoggerDecorator : IProxyService
{
    private readonly IProxyService _proxy;
    private readonly ILogger _logger;

    public ProxyServiceLoggerDecorator(IProxyService proxy, ILogger<ProxyServiceLoggerDecorator> logger)
    {
        _proxy = proxy;
        _logger = logger;
    }

    public async Task<OneOf<ProxyGrpcRequestResultDto, RpcException, NotFound>> Proxy(
        ProxyGrpcRequestQueryDto query, CancellationToken cancellationToken = default)
    {
        var timer = ValueStopwatch.StartNew();
        try
        {
            var result = await _proxy.Proxy(query, cancellationToken);
            var elapsed = timer.GetElapsedTime().TotalMilliseconds;
            result.Switch(
                result =>
                {
                    _logger.LogInformation("Proxy request for service {ServiceShortName} and method {MethodName} is completed successfully in {Elapsed:0.0000}ms.", query.ServiceShortName, query.MethodName, elapsed);
                },
                rpcException =>
                {
                    _logger.LogInformation("Proxy request for service {ServiceShortName} and method {MethodName} is completed with RpcException in {Elapsed:0.0000}ms.", query.ServiceShortName, query.MethodName, elapsed);
                },
                notFound =>
                {
                    _logger.LogInformation("Proxy request is completed for service {ServiceShortName} and method {MethodName} with NotFount status in {Elapsed:0.0000}ms.", query.ServiceShortName, query.MethodName, elapsed);
                }
            );

            return result;
        }
        catch
        {
            _logger.LogError("Proxy request with {@Query} is failed in {Elapsed:0.0000}ms.", query, timer.GetElapsedTime().TotalMilliseconds);
            throw;
        }
    }
}
