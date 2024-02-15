using Google.Protobuf;
using Grpc.Core;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubResponse;

public record ReadStubResponseQueryResult(
    IMessage Body,
    Status Status,
    Metadata Metadata);
