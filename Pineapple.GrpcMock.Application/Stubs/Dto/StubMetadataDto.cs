using System.Text.Json;

namespace Pineapple.GrpcMock.Application.Stubs.Dto;

public record StubMetadataDto(
    IReadOnlyDictionary<string, JsonElement> Trailer);