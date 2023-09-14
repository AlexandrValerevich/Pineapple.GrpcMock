using System.Collections.Immutable;
using System.Reflection;
using Pineapple.GrpcMock.Application.Common.GrpcServices.Registry;
using Pineapple.GrpcMock.Application.Common.GrpcServices.Registry.Dto;
using Pineapple.GrpcMock.Infrastructure.GrpcServices.Registry.Extensions;

namespace Pineapple.GrpcMock.Infrastructure.GrpcServices.Registry;

public class GrpcServicesRegistry : IGrpcServicesRegistry
{
    private static readonly Lazy<IList<GrpcServiceMetaDto>> _services = new(() =>
    {
        return Assembly.GetExecutingAssembly().GetGrpcServices()
            .Select(g => new GrpcServiceMetaDto
            {
                ShortName = g.Name[..^4],
                Methods = g.GetMethods().Select(m => new GrpcServiceMethodMetaDto
                {
                    Name = m.Name,
                    InputType = m.GetParameters().First(x => x.Position == 0).ParameterType,
                    OutputType = m.ReturnType.GenericTypeArguments.First()
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
