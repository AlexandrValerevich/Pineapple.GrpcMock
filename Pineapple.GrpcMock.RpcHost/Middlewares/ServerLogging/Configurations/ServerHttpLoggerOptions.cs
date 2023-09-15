using Microsoft.Net.Http.Headers;

namespace Pineapple.GrpcMock.RpcHost.Middlewares.ServerLogging.Configurations;

public class ServerHttpLoggerOptions
{
    public const string SectionName = nameof(ServerHttpLoggerOptions);

    public int RequestBodyLogLimit { get; set; } = 32 * 1024;

    public int ResponseBodyLogLimit { get; set; } = 32 * 1024;

    public HashSet<string> RequestHeaders { get; set; } = new(27, StringComparer.OrdinalIgnoreCase)
    {
        HeaderNames.Accept,
        HeaderNames.AcceptCharset,
        HeaderNames.AcceptEncoding,
        HeaderNames.AcceptLanguage,
        HeaderNames.Allow,
        HeaderNames.CacheControl,
        HeaderNames.Connection,
        HeaderNames.ContentEncoding,
        HeaderNames.ContentLength,
        HeaderNames.ContentType,
        HeaderNames.Date,
        HeaderNames.Expect,
        HeaderNames.Host,
        HeaderNames.TraceParent,
        HeaderNames.MaxForwards,
        HeaderNames.Range,
        HeaderNames.TE,
        HeaderNames.Trailer,
        HeaderNames.TransferEncoding,
        HeaderNames.Upgrade,
        HeaderNames.UserAgent,
        HeaderNames.Warning,
        HeaderNames.XRequestedWith,
    };

    public HashSet<string> ResponseHeaders { get; set; } = new(26, StringComparer.OrdinalIgnoreCase)
    {
        HeaderNames.AcceptRanges,
        HeaderNames.Age,
        HeaderNames.Allow,
        HeaderNames.Connection,
        HeaderNames.ContentDisposition,
        HeaderNames.ContentLanguage,
        HeaderNames.ContentLength,
        HeaderNames.ContentLocation,
        HeaderNames.ContentRange,
        HeaderNames.ContentType,
        HeaderNames.Date,
        HeaderNames.Expires,
        HeaderNames.LastModified,
        HeaderNames.Location,
        HeaderNames.Server,
        HeaderNames.TransferEncoding,
        HeaderNames.Upgrade,
    };
}