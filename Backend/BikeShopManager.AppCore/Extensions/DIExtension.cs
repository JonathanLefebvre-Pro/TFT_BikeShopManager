using BikeShopManager.AppCore.Features.Connection;
using BikeShopManager.AppCore.Features.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopManager.AppCore.Extensions;

public static class DIExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDbSession, DbSession>();
        services.AddScoped<Dispatcher>();
        services.AddScoped<IDispatcher, TransactionalDispatcherDecorator>(s =>
        {
            var dispatcher = s.GetRequiredService<Dispatcher>();
            var dbSession = s.GetRequiredService<IDbSession>();

            return new TransactionalDispatcherDecorator(dispatcher, dbSession);
        });

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
