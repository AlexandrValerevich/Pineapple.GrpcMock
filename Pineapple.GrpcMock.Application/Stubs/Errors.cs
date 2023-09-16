using ErrorOr;

namespace Pineapple.GrpcMock.Application.Stubs;

public static partial class Errors
{
    public static class Stubs
    {
        public readonly static Error NotFound = Error.NotFound("Stubs.NotFound", "Stubs not found.");
    }
}
