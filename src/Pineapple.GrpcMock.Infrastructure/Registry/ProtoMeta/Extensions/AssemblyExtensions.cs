using System.Reflection;
using Grpc.Core;

namespace Pineapple.GrpcMock.Infrastructure.Registry.ProtoMeta.Extensions;

internal static class AssemblyExtensions
{
    public static IReadOnlyList<Type> GetProtoServices(this Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(x => x.GetCustomAttributes<BindServiceMethodAttribute>(true).Any())
            .ToList();
    }

    public static IReadOnlyList<Type> GetProtoServicesClients(this Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(x =>
                x.BaseType is not null &&
                x.BaseType.IsGenericType &&
                x.BaseType.GetGenericTypeDefinition() == typeof(ClientBase<>))
            .ToList();
    }
}