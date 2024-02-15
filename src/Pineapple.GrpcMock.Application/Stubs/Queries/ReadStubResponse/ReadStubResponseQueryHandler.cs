using Grpc.Core;
using Mediator;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Proxies;
using Pineapple.GrpcMock.Application.Proxies.Services.Dto;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubResponse;

public class ReadStubResponseQueryHandler : IQueryHandler<ReadStubResponseQuery, OneOf<ReadStubResponseQueryResult, RpcException, NotFound>>
{
    private readonly IStubRegistry _stubs;
    private readonly IProxyService _proxyService;
    private readonly ILogger _logger;

    public ReadStubResponseQueryHandler(IStubRegistry stubs, ILogger<ReadStubResponseQueryHandler> logger, IProxyService proxyService)
    {
        _stubs = stubs;
        _logger = logger;
        _proxyService = proxyService;
    }

    public async ValueTask<OneOf<ReadStubResponseQueryResult, RpcException, NotFound>> Handle(ReadStubResponseQuery query, CancellationToken cancellationToken)
    {
        var shortName = query.ServiceFullName.Split(".").Last();
        var values = _stubs.Get(new(
            ServiceShortName: shortName,
            Method: query.Method));

        StubRegistryValueDto? value = values.Where(x => x.Request.Equals(query.Request)).MaxBy(x => x.Priority);
        if (value is null)
        {
            var proxyResult = _proxyService.Proxy(new ProxyGrpcRequestQueryDto(
                ServiceShortName: shortName,
                MethodName: query.Method,
                Request: query.Request));

            return proxyResult.Match<OneOf<ReadStubResponseQueryResult, RpcException, NotFound>>(
                result => new ReadStubResponseQueryResult(
                    Body: result.Response,
                    Status: result.Status,
                    Metadata: result.Metadata),
                rpcException => rpcException,
                notFound => notFound
            );
        }

        if (value.Delay != TimeSpan.Zero)
        {
            _logger.LogDebug("Response delayed on {Delay:0.00}ms", value.Delay.TotalMilliseconds);
            await Task.Delay(value.Delay, cancellationToken);
        }

        return new ReadStubResponseQueryResult(
            Body: value.Response,
            Status: value.Status,
            Metadata: value.Metadata);
    }
}