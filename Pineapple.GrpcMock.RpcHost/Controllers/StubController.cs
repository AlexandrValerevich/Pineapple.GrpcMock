using System.Collections.Immutable;
using System.Net.Mime;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Pineapple.GrpcMock.Application.Stubs.Commands.AddStub;
using Pineapple.GrpcMock.Application.Stubs.Dto;
using Pineapple.GrpcMock.Contracts.Stubs.V1;

namespace Pineapple.GrpcMock.RpcHost.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("api/v1/__admin/stubs")]
public sealed class StubController : ControllerBase
{
    private readonly IMediator _mediator;

    public StubController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddStubApiRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(
           new AddStubCommand(
                ServiceShortName: request.ServiceShortName,
                Method: request.ServiceMethod,
                RequestBody: request.Request.Body.ToString(),
                ResponseBody: request.Response.Body.ToString(),
                Status: new StubStatusDto(
                    Code: request.Response.Status.Code,
                    Details: request.Response.Status.Details),
                Metadata: new StubMetadataDto(
                    Trailer: request.Response.Metadata.Trailer.ToImmutableDictionary()),
                Delay: request.Response.Delay),
           cancellationToken);

        return Ok();
    }
}
