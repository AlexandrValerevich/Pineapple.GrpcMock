using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace Pineapple.GrpcMock.RpcHost.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/v1/__admin/proto")]
internal sealed class ProtoMetaController : ControllerBase
{

}
