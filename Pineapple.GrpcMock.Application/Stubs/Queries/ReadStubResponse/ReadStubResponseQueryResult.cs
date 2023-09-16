using Google.Protobuf;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubResponse;

public record ReadStubResponseQueryResult(
    IMessage Response);