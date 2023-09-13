using Microsoft.AspNetCore.Mvc;

namespace Pineapple.GrpcMock.RpcHost.Controllers;

[ApiController]
[Consumes("application/grpc+proto", "application/grpc")]
public sealed class StubController : ControllerBase
{

    [HttpPost("/{serviceName}/{serviceMethod}")]
    public Task<string> Stub([FromRoute] string serviceName, [FromRoute] string serviceMethod)
    {
        using var streamReader = new StreamReader(Request.BodyReader.AsStream());
        var requestBody = streamReader.ReadToEnd();
        var shortServiceName = serviceName.Split(".").Last();

        return Task.FromResult($"{shortServiceName}/{serviceMethod}/{requestBody}");
    }

}
