using Mediator;
using Microsoft.AspNetCore.Mvc;
using Pineapple.GrpcMock.Application.Stubs.Queries;

namespace Pineapple.GrpcMock.RpcHost.Controllers;

[ApiController]
[Consumes("application/grpc+proto", "application/grpc")]
public sealed class StubController : ControllerBase
{
    private readonly IMediator _mediator;

    public StubController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/{serviceFullName}/{method}")]
    public async Task<byte[]> Stub([FromRoute] string serviceFullName, [FromRoute] string method)
    {
        using var memoryStream = new MemoryStream();
        Request.BodyReader.AsStream().CopyTo(memoryStream);
        var requestBody = memoryStream.ToArray();

        var result = await _mediator.Send(new ReadStubResponseQuery(
            ServiceFullName: serviceFullName,
            Method: method,
            RequestBody: requestBody
        ));

        return result.Response;
    }

}
