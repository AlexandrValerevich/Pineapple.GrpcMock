using Mediator;
using Microsoft.Extensions.Logging;
using Pineapple.GrpcMock.RpcHost.Shared.Helpers;

namespace Pineapple.GrpcMock.Application.ProtoMeta.Queries.ReadProtoMetaList;

internal sealed class ReadProtoMetaListQueryHandlerLoggingBehavior : IPipelineBehavior<ReadProtoMetaListQuery, ReadProtoMetaListQueryResult>
{
    private readonly ILogger _logger;

    public ReadProtoMetaListQueryHandlerLoggingBehavior(ILogger<ReadProtoMetaListQueryHandlerLoggingBehavior> logger)
    {
        _logger = logger;
    }

    public async ValueTask<ReadProtoMetaListQueryResult> Handle(ReadProtoMetaListQuery message, MessageHandlerDelegate<ReadProtoMetaListQuery, ReadProtoMetaListQueryResult> next, CancellationToken cancellationToken)
    {
        var timer = ValueStopwatch.StartNew();
        try
        {
            _logger.LogTrace("Read grpc services list request.");
            var response = await next(message, cancellationToken);
            _logger.LogInformation("Read grpc services list request is completed in {Elapsed:0.0000}ms.", timer.GetElapsedTime().TotalMilliseconds);

            return response;
        }
        catch
        {
            _logger.LogError("Read grpc services list for [{ServiceName}/{Method}] is failed in {Elapsed:0.0000}ms.", timer.GetElapsedTime().TotalMilliseconds);
            throw;
        }
    }
}
