using Mediator;

namespace Pineapple.GrpcMock.Application.Stubs.Queries;

public record ReadStubResponseQuery(
    string ServiceFullName,
    string Method,
    byte[] RequestBody
) : IQuery<ReadStubResponseQueryResult>;

