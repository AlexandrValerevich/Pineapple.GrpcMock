using System.Collections.Immutable;
using System.Net.Mime;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Pineapple.GrpcMock.Application.ProtoMeta.Queries.ReadProtoMetaList;
using Pineapple.GrpcMock.Contracts.ProtoMeta.V1;

namespace Pineapple.GrpcMock.RpcHost.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/v1/__admin/proto")]
public sealed class ProtoMetaController : ApiController
{
    private readonly IMediator _mediator;

    public ProtoMetaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetProtoMetaListApiResponse>> List()
    {
        ReadProtoMetaListQueryResult response = await _mediator.Send(ReadProtoMetaListQuery.Instance);
        return Ok(new GetProtoMetaListApiResponse
        {
            Services = response.ServicesMeta.Select(s => new ProtoServiceMetaApiModel
            {
                ShortName = s.ShortName,
                Methods = s.Methods.Select(m => new ProtoMethodMetaApiModel()
                {
                    Name = m.Name
                }).ToImmutableList()
            }).ToImmutableList()
        });
    }
}