using System.Collections.Immutable;
using System.Reflection;
using Grpc.Core;
using Pineapple.GrpcMock.Application.Common.GrpcServices.Registry;
using Pineapple.GrpcMock.Application.Common.GrpcServices.Registry.Dto;
using Pineapple.GrpcMock.Infrastructure.GrpcServices.Registry.Extensions;
using Pineapple.GrpcMock.Protos;

namespace Pineapple.GrpcMock.Infrastructure.GrpcServices.Registry;

public class GrpcServicesRegistry : IGrpcServicesRegistry
{
    private static readonly Lazy<IList<GrpcServiceMetaDto>> _services = new(() =>
    {
        var services = Assembly.GetAssembly(typeof(IAssemblyMarker))!.GetGrpcServices();
        return services.Select(s => new GrpcServiceMetaDto
        {
            ShortName = s.Name[..^4],
            Methods = s.GetMethods()
                .Where(x => x.GetParameters().Any(
                    x => x.Position == 2 && x.ParameterType.Equals(typeof(ServerCallContext))))
                .Select(m =>
                {
                    ParameterInfo[] parameters = m.GetParameters();
                    return new GrpcServiceMethodMetaDto
                    {
                        Name = m.Name,
                        InputType = parameters.First().ParameterType,
                        OutputType = m.ReturnType.GenericTypeArguments.First()
                    };
                }).ToImmutableList()
        }).ToImmutableList();
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
