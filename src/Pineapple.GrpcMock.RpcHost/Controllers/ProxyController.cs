using System.Collections.Immutable;
using System.Net.Mime;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Pineapple.GrpcMock.Application.Proxies.Commands.AddOrUpdateProxy;
using Pineapple.GrpcMock.Application.Proxies.Commands.DeleteProxy;
using Pineapple.GrpcMock.Application.Proxies.Queries.ReadProxyList;
using Pineapple.GrpcMock.Contracts.Proxies.V1;

namespace Pineapple.GrpcMock.RpcHost.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/v1/__admin/proxies")]
public sealed class ProxyController : ApiController
{
    private readonly IMediator _mediator;

    public ProxyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ReadProxyListApiResponse>> AddOrUpdate(CancellationToken cancellationToken)
    {
        ReadProxyListQueryResult result = await _mediator.Send(new ReadProxyListQuery(), cancellationToken);
        return new ReadProxyListApiResponse
        {
            Proxies = result.Proxies.Select(x => new ProxyItemApiModel
            {
                ProxyTo = x.ProxyTo,
                ServiceShortName = x.ServiceShortName
            }).ToImmutableArray()
        };
    }

    [HttpPut]
    public async Task<IActionResult> AddOrUpdate([FromBody] AddOrUpdateProxyApiRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AddOrUpdateProxyCommand(
            ServiceShortName: request.ServiceShortName,
            ProxyUrl: request.ProxyTo
        ), cancellationToken);

        return result.Match(
            success => Ok(),
            Problem
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteProxyApiRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteProxyCommand(ServiceShortName: request.ServiceShortName), cancellationToken);
        return Ok();
    }
}
