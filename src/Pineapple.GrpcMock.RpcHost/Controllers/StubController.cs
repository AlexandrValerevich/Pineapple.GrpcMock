using System.Collections.Immutable;
using System.Net.Mime;
using System.Text.Json;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Pineapple.GrpcMock.Application.Stubs.Commands.AddStub;
using Pineapple.GrpcMock.Application.Stubs.Commands.RemoveStubList;
using Pineapple.GrpcMock.Application.Stubs.Dto;
using Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubList;
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
                Method: request.Method,
                Priority: request.Priority,
                RequestBody: request.Request.Body,
                ResponseBody: request.Response.Body,
                Status: new StubStatusDto(
                    Code: request.Response.Status.Code,
                    Details: request.Response.Status.Details),
                Metadata: new StubMetadataDto(
                    Trailer: request.Response.Metadata.Trailer.ToImmutableDictionary()),
                Delay: request.Response.Delay),
           cancellationToken);

        return Ok();
    }


    [HttpGet]
    public async Task<ActionResult<ReadStubListApiResponse>> List(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(ReadStubListQuery.Instance, cancellationToken);

        return Ok(new ReadStubListApiResponse
        {
            Stubs = result.Stubs.Select(x => new StubItemApiModel
            {
                ServiceShortName = x.ServiceShortName,
                Method = x.Method,
                Priority = x.Priority,
                Request = new StubRequestApiModel
                {
                    Body = x.RequestBody
                },
                Response = new StubResponseApiModel
                {
                    Body = x.ResponseBody,
                    Delay = x.Delay,
                    Metadata = new StubMetadataApiModel
                    {
                        Trailer = (x.Metadata.Trailer as IDictionary<string, JsonElement>)!
                    },
                    Status = new StubStatusApiModel
                    {
                        Code = x.Status.Code,
                        Details = x.Status.Details
                    }
                }
            }).ToImmutableList()
        });
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveList([FromBody] RemoveStubListApiRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(
           new RemoveStubListCommand(
                ServiceShortName: request.ServiceShortName,
                Method: request.Method),
           cancellationToken);

        return Ok();
    }

}
