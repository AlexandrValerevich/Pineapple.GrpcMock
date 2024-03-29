using System.Collections.Immutable;
using System.Reflection;
using Grpc.Core;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.ProtoMeta.Dto;
using Pineapple.GrpcMock.Infrastructure.Registry.ProtoMeta.Extensions;
using Pineapple.GrpcMock.Protos;

namespace Pineapple.GrpcMock.Infrastructure.Registry.ProtoMeta;

public class ProtoMetaRegistry : IProtoMetaRegistry
{
    private static readonly Lazy<IList<ProtoServiceMetaDto>> _services = new(() =>
    {
        IReadOnlyList<Type> services = Assembly.GetAssembly(typeof(IAssemblyMarker))!.GetProtoServices();
        IReadOnlyList<Type> clients = Assembly.GetAssembly(typeof(IAssemblyMarker))!.GetProtoServicesClients();
        return services.Select(s => new ProtoServiceMetaDto(
            ServiceType: s,
            ClientType: clients.First(client => client.Name[..^"Client".Length] == s.Name[..^"Base".Length]),
            ShortName: s.Name[..^"Base".Length],
            Methods: s.GetMethods()
                .Where(x => x.GetParameters().Any(
                    x => x.Position == 1 && x.ParameterType.Equals(typeof(ServerCallContext))))
                .Select(m =>
                {
                    var parameters = m.GetParameters();
                    return new ProtoMethodMetaDto(
                        Name: m.Name,
                        InputType: parameters.First().ParameterType,
                        OutputType: m.ReturnType.GenericTypeArguments.First());
                }).ToImmutableList())
            ).ToImmutableList();
    });

    public ProtoServiceMetaDto? Get(string shortName)
    {
        return _services.Value.SingleOrDefault(x => x.ShortName == shortName);
    }

    public IReadOnlyList<ProtoServiceMetaDto> List()
    {
        return _services.Value.ToImmutableList();
    }
}