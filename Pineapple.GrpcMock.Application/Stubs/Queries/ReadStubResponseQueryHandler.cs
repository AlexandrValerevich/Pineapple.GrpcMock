using Mediator;
using Pineapple.GrpcMock.Application.Stubs.Registry;
using Pineapple.GrpcMock.Application.Stubs.Registry.Dto;
using Throw;

namespace Pineapple.GrpcMock.Application.Stubs.Queries;

public class ReadStubResponseQueryHandler : IQueryHandler<ReadStubResponseQuery, ReadStubResponseQueryResult>
{
    private readonly IStubRegistry _stubs;

    public ReadStubResponseQueryHandler(IStubRegistry stubs)
    {
        _stubs = stubs;
    }

    public ValueTask<ReadStubResponseQueryResult> Handle(ReadStubResponseQuery query, CancellationToken cancellationToken)
    {
        var shortName = query.ServiceFullName.Split(".").Last();

        StubRegistryValueDto value = _stubs.Get(new(
            ServiceShortName: shortName,
            Method: query.Method,
            RequestBody: query.RequestBody.All(x => x != 0) ? query.RequestBody : Array.Empty<byte>()
        )).ThrowIfNull();

        return ValueTask.FromResult(new ReadStubResponseQueryResult(
            Response: value.Response
        ));
    }
}
