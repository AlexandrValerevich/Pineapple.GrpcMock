using ErrorOr;

namespace Pineapple.GrpcMock.Application.Stubs;

public static partial class Errors
{
    public static class Stubs
    {
        public readonly static Error StubNotFound = Error.NotFound("Stubs.StubNotFound", "Stubs not found.");
    }
}
