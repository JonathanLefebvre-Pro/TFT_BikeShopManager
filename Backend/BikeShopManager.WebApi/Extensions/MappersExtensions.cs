using BikeShopManager.AppCore.Interfaces.Mappers;
using Scrutor;

namespace BikeShopManager.WebApi.Extensions;

public static class MappersExtensions
{
    public static IServiceCollection AddMapperServices(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan.FromAssembliesOf(typeof(MappersExtensions))
                .AddClasses(classes =>
                    classes.AssignableToAny(typeof(IEntityMapper<,>), typeof(IDtoMapper<,>))
                )
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        return services;
    }
}
