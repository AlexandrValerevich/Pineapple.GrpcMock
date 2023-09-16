using ErrorOr;
using Mediator;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Queries;

public class ReadStubResponseQueryHandler : IQueryHandler<ReadStubResponseQuery, ErrorOr<ReadStubResponseQueryResult>>
{
    private readonly IStubRegistry _stubs;

    public ReadStubResponseQueryHandler(IStubRegistry stubs)
    {
        _stubs = stubs;
    }

    public ValueTask<ErrorOr<ReadStubResponseQueryResult>> Handle(ReadStubResponseQuery query, CancellationToken cancellationToken)
    {
        string shortName = query.ServiceFullName.Split(".").Last();

        IReadOnlyList<StubRegistryValueDto> values = _stubs.Get(new(
            ServiceShortName: shortName,
            Method: query.Method));

        StubRegistryValueDto? value = values.FirstOrDefault(x => x.Request.Equals(query.Request));

        if (value is null)
            return ValueTask.FromResult<ErrorOr<ReadStubResponseQueryResult>>(Errors.Stubs.NotFound);

        return ValueTask.FromResult<ErrorOr<ReadStubResponseQueryResult>>(
            new ReadStubResponseQueryResult(Response: value.Response));
    }
}