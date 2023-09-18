using System.Text.Json;

namespace Pineapple.GrpcMock.Application.Stubs.Dto;

public record StubItemDto(
    string ServiceShortName,
    string Method,
    int Priority,
    JsonElement RequestBody,
    JsonElement ResponseBody,
    StubStatusDto Status,
    StubMetadataDto Metadata,
    TimeSpan Delay);