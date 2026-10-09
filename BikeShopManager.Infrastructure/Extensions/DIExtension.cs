using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopManager.Infrastructure.Extensions;

public static class DIExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDbSession, DbSession>();
        services.Scan(scan =>
            scan.FromAssemblies(typeof(DIExtension).Assembly)
                .AddClasses(classes =>
                    classes.AssignableToAny(
                        typeof(ICommandHandlerAsync<>),
                        typeof(ICommandHandlerAsync<,>),
                        typeof(IQueryHandlerAsync<,>)
                    )
                )
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        return services;
    }
}
