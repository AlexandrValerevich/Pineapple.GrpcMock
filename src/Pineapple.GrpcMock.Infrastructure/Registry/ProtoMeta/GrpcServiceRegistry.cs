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
        var services = Assembly.GetAssembly(typeof(IAssemblyMarker))!.GetProtoMeta();
        return services.Select(s => new ProtoServiceMetaDto(
            ServiceType: s,
            ShortName: s.Name[..^4],
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