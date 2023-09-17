using System.Reflection;
using Grpc.Core;

namespace Pineapple.GrpcMock.Infrastructure.Registry.ProtoMeta.Extensions;

internal static class AssemblyExtensions
{
    public static IEnumerable<Type> GetProtoMeta(this Assembly assembly)
    {
        return assembly.GetTypes().Where(x => x.GetCustomAttributes<BindServiceMethodAttribute>(true).Any());
    }
}