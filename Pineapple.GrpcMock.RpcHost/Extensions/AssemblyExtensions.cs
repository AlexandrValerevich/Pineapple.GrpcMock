using System.Reflection;
using Grpc.Core;

namespace Pineapple.GrpcMock.RpcHost.Extensions;

internal static class AssemblyExtensions
{
    public static IEnumerable<Type> GetGrpcServices(this Assembly assembly)
    {
        return assembly.GetTypes().Where(x => x.GetCustomAttributes<BindServiceMethodAttribute>(true).Any());
    }
}
