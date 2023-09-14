namespace Pineapple.GrpcMock.Application.Common.Stub.Registry.Dto;

public record StubRegistryKeyDto
(
    string ServiceShortName,
    string Method,
    byte[] RequestBody
)
{
    public override int GetHashCode()
    {
        return ServiceShortName.GetHashCode() + Method.GetHashCode() + RequestBody.Sum(x => x);
    }
}