using ErrorOr;
using Mediator;
using Pineapple.GrpcMock.Application.Common.Registry;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubResponse;

public class ReadStubResponseQueryHandler : IQueryHandler<ReadStubResponseQuery, ErrorOr<ReadStubResponseQueryResult>>
{
    private readonly IStubRegistry _stubs;

    public ReadStubResponseQueryHandler(IStubRegistry stubs)
    {
        _stubs = stubs;
    }

    public ValueTask<ErrorOr<ReadStubResponseQueryResult>> Handle(ReadStubResponseQuery query, CancellationToken cancellationToken)
    {
        var shortName = query.ServiceFullName.Split(".").Last();

        var values = _stubs.Get(new(
            ServiceShortName: shortName,
            Method: query.Method));

        var value = values.FirstOrDefault(x => x.Request.Equals(query.Request));

        if (value is null)
            return ValueTask.FromResult<ErrorOr<ReadStubResponseQueryResult>>(Errors.Stubs.NotFound);

        return ValueTask.FromResult<ErrorOr<ReadStubResponseQueryResult>>(
            new ReadStubResponseQueryResult(Response: value.Response));
    }
}