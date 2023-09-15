using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace Pineapple.GrpcMock.RpcHost.OutputFormatters;

internal sealed class GrpcOutputFormatter : OutputFormatter
{
    public GrpcOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/grpc"));
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/grpc+proto"));
    }

    public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
    {
        Serilog.Log.Verbose("Use my output formatter");
        return Task.CompletedTask;
    }
}
