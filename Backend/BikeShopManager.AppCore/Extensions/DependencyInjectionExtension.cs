using System.Reflection;
using BikeShopManager.AppCore.Features.Commands.Stock;
using BikeShopManager.AppCore.Features.Connection;
using BikeShopManager.AppCore.Features.Dispatcher;
using BikeShopManager.AppCore.Features.Queries.Stock;
using BikeShopManager.AppCore.Features.Tokens;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Tokens;
using BikeShopManager.Domain.Entities.Stock;
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

        //Add generic CQS handlers
        services.AddGenericHandlers(
            typeof(ICommandHandlerAsync<>),
            typeof(StockEntityBase).Assembly
        );
        services.AddGenericHandlers(
            typeof(ICommandHandlerAsync<,>),
            typeof(StockEntityBase).Assembly
        );
        services.AddGenericHandlers(
            typeof(IQueryHandlerAsync<,>),
            typeof(StockEntityBase).Assembly
        );

        //Add non-generic CQS handlers
        services.Scan(scan =>
            scan.FromAssemblies(typeof(DependencyInjectionExtension).Assembly)
                .AddClasses(classes =>
                    classes.AssignableToAny(
                        typeof(ICommandHandlerAsync<>),
                        typeof(ICommandHandlerAsync<,>),
                        typeof(IQueryHandlerAsync<,>)
                    )
                )
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        return services;
    }

    public static IServiceCollection AddGenericHandlers(
        this IServiceCollection services,
        Type handlerInterface,
        Assembly assembly
    )
    {
        //Get all types that derived from StockEntityBase, that are concrete classes and not abstract classes
        IReadOnlyList<Type> entityTypes = assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(StockEntityBase)))
            .ToList();

        //Get all handlers that are concrete classes, not abstract classes,
        // that have a generic type definition
        IEnumerable<Type>? openHandlers = handlerInterface
            .Assembly.GetTypes()
            .Where(t =>
                t is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: true }
                && t.GetGenericArguments().Length == 1
                && t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            );

        foreach (Type openType in openHandlers)
        {
            foreach (Type entityType in entityTypes)
            {
                Type closed;
                try
                {
                    closed = openType.MakeGenericType(entityType);
                }
                catch (ArgumentException)
                {
                    continue;
                }

                foreach (
                    Type service in closed
                        .GetInterfaces()
                        .Where(i =>
                            i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface
                        )
                )
                {
                    services.AddScoped(service, closed);
                }
            }
        }

        return services;
    }
}
