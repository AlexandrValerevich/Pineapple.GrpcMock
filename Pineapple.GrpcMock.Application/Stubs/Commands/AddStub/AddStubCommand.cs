using System.Text.Json;
using ErrorOr;
using Mediator;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.AddStub;

public record AddStubCommand(
    string ServiceShortName,
    string Method,
    JsonElement RequestBody,
    JsonElement ResponseBody,
    StubStatusDto Status,
    StubMetadataDto Metadata,
    TimeSpan Delay) : ICommand<ErrorOr<Unit>>;