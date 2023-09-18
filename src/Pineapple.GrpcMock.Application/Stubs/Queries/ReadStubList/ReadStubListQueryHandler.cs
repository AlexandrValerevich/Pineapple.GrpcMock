using System.Collections.Immutable;
using Mediator;
using Pineapple.GrpcMock.Application.Common.Converter;
using Pineapple.GrpcMock.Application.Common.Extensions;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubList;

internal sealed class ReadStubListQueryHandler : IQueryHandler<ReadStubListQuery, ReadStubListQueryResult>
{
    private readonly IStubRegistry _stubs;
    private readonly IProtobufConverter _converter;

    public ReadStubListQueryHandler(IStubRegistry stubs, IProtobufConverter converter)
    {
        _stubs = stubs;
        _converter = converter;
    }

    public ValueTask<ReadStubListQueryResult> Handle(ReadStubListQuery query, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(new ReadStubListQueryResult(
            Stubs: _stubs.List().SelectMany(kvp =>
            {
                var items = new List<StubItemDto>();

                foreach (var value in kvp.Value)
                {
                    var item = new StubItemDto(
                        ServiceShortName: kvp.Key.ServiceShortName,
                        Method: kvp.Key.Method,
                        Priority: value.Priority,
                        RequestBody: _converter.ToJsonElement(value.Request),
                        ResponseBody: _converter.ToJsonElement(value.Response),
                        Status: new StubStatusDto(
                            Code: (int) value.Status.StatusCode,
                            Details: value.Status.Detail
                        ),
                        Metadata: new StubMetadataDto(
                            Trailer: value.Metadata.ToReadOnlyDictionary()),
                        Delay: value.Delay
                    );
                    items.Add(item);
                }
                return items;
            }
            ).ToImmutableList()
        ));
    }
}
