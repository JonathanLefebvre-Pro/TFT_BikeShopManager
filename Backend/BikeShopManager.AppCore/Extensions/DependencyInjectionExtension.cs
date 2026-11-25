using BikeShopManager.AppCore.Features.Connection;
using BikeShopManager.AppCore.Features.Dispatcher;
using BikeShopManager.AppCore.Features.Tokens;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace BikeShopManager.AppCore.Extensions.DependencyInjection;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IJwtUtilities, JwtUtilities>();
        services.AddScoped<IDbSession, DbSession>();
        services.AddScoped<Dispatcher>();
        services.AddScoped<IDispatcher, TransactionalDispatcherDecorator>(s =>
        {
            var dispatcher = s.GetRequiredService<Dispatcher>();
            var dbSession = s.GetRequiredService<IDbSession>();

            return new TransactionalDispatcherDecorator(dispatcher, dbSession);
        });

        services.Scan(scan =>
            scan.FromAssemblies(typeof(DependencyInjectionExtension).Assembly)
                .AddClasses(classes =>
                    classes.AssignableToAny(
                        typeof(ICommandHandlerAsync<>),
                        typeof(ICommandHandlerAsync<,>),
                        typeof(IQueryHandlerAsync<,>)
                    )
                //.Where(_ => !_.IsGenericType)
                )
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        return services;
    }
}
