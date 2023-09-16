using Mediator;
using Pineapple.GrpcMock.Application.Common.Registry;

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
        var values = _stubs.Get(new(
            ServiceShortName: shortName,
            Method: query.Method));

        return ValueTask.FromResult(new ReadStubResponseQueryResult(Response: values[0].Response));
    }
}