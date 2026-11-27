using System.Reflection;
using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.WebApi.Extensions;

public static class MappersExtensions
{
    public static IServiceCollection AddMapperServices(this IServiceCollection services)
    {
        services.AddGenericMappers(typeof(IEntityMapper<,>), typeof(MappersExtensions).Assembly);
        services.AddGenericMappers(typeof(IDtoMapper<,>), typeof(MappersExtensions).Assembly);

        return services;
    }

    private static IServiceCollection AddGenericMappers(
        this IServiceCollection services,
        Type mapperInterface,
        Assembly assembly
    )
    {
        IReadOnlyList<Type> entityTypes = typeof(StockEntityBase)
            .Assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(StockEntityBase)))
            .ToList();

        IReadOnlyList<Type> dtoTypes = assembly
            .GetTypes()
            .Where(t =>
                t is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false }
                && t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDto<>))
            )
            .ToList();

        IEnumerable<Type> openMappers = assembly
            .GetTypes()
            .Where(t =>
                t is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false }
                && t.GetInterfaces()
                    .Any(i =>
                        i.IsGenericType
                        && i.GetGenericArguments().Length == 2
                        && i.GetGenericTypeDefinition() == mapperInterface
                    )
            );

        foreach (Type openType in openMappers)
        {
            foreach (Type entityType in entityTypes)
            {
                foreach (Type dtoType in dtoTypes)
                {
                    Type closed;
                    try
                    {
                        closed = openType.MakeGenericType(entityType, dtoType);
                    }
                    catch (ArgumentException)
                    {
                        continue;
                    }

                    foreach (
                        Type service in closed
                            .GetInterfaces()
                            .Where(i =>
                                i.IsGenericType && i.GetGenericTypeDefinition() == mapperInterface
                            )
                    )
                    {
                        services.AddSingleton(service, closed);
                    }
                }
            }
        }

        return services;
    }
}
