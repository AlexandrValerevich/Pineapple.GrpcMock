using System.Collections.Immutable;
using System.Reflection;
using Grpc.Core;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.GrpcServices.Dto;
using Pineapple.GrpcMock.Infrastructure.Registry.GrpcServices.Extensions;
using Pineapple.GrpcMock.Protos;

namespace Pineapple.GrpcMock.Infrastructure.Registry.GrpcServices;

public class GrpcServiceRegistry : IGrpcServiceRegistry
{
    private static readonly Lazy<IList<GrpcServiceMetaDto>> _services = new(() =>
    {
        var services = Assembly.GetAssembly(typeof(IAssemblyMarker))!.GetGrpcServices();
        return services.Select(s => new GrpcServiceMetaDto(
            ServiceType: s,
            ShortName: s.Name[..^4],
            Methods: s.GetMethods()
                .Where(x => x.GetParameters().Any(
                    x => x.Position == 1 && x.ParameterType.Equals(typeof(ServerCallContext))))
                .Select(m =>
                {
                    var parameters = m.GetParameters();
                    return new GrpcServiceMethodMetaDto(
                        Name: m.Name,
                        InputType: parameters.First().ParameterType,
                        OutputType: m.ReturnType.GenericTypeArguments.First());
                }).ToImmutableList())
            ).ToImmutableList();
    });

    public GrpcServiceMetaDto? Get(string shortName)
    {
        return _services.Value.SingleOrDefault(x => x.ShortName == shortName);
    }

    public IReadOnlyList<GrpcServiceMetaDto> List()
    {
        return _services.Value.ToImmutableList();
    }
}