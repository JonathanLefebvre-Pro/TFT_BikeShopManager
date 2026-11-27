using System.Reflection;
using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.WebApi.Extensions;

public static class MappersExtensions
{
    public static IServiceCollection AddMapperServices(this IServiceCollection services)
    {
        services.AddGenericMappers(typeof(MappersExtensions).Assembly);

        return services;
    }

    private static IServiceCollection AddGenericMappers(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        // Uniquement les mappers concrets : les bases abstraites et les génériques
        // ouverts (StockDtoMapperBase<,>) ne sont pas instanciables.
        IEnumerable<Type> mappers = assembly
            .GetTypes()
            .Where(t =>
                t is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false }
                && t.GetInterfaces().Any(IsMapper)
            )
            .ToArray();

        foreach (Type mapperType in mappers)
        {
            // Enregistrement du type concret : c'est lui qui porte l'unique instance.
            services.AddSingleton(mapperType);

            // Une boucle plutôt qu'un Single : une classe peut implémenter
            // à la fois IEntityMapper et IDtoMapper.
            foreach (Type mapperInterface in mapperType.GetInterfaces().Where(IsMapper))
            {
                // Alias vers l'instance concrète, pour qu'il n'y ait pas une
                // instance par interface.
                services.AddSingleton(mapperInterface, sp => sp.GetRequiredService(mapperType));

                // La covariance (out TDto) n'existe que pour IDtoMapper.
                // IEntityMapper est résolu avec le type concret du DTO.
                if (mapperInterface.GetGenericTypeDefinition() != typeof(IDtoMapper<,>))
                    continue;

                Type[] args = mapperInterface.GetGenericArguments();
                Type dtoContract = typeof(IDto<>).MakeGenericType(args[0]);

                // Le conteneur DI ne tient pas compte de la variance : il faut
                // enregistrer explicitement chaque interface DTO (IResponseDto<Article>...)
                // sous laquelle le mapper doit être résolu.
                // IDto<> est exclu car c'est l'interface marqueur commune, trop générique.
                IEnumerable<Type> dtoInterfaces = args[1]
                    .GetInterfaces()
                    .Where(i =>
                        i.IsGenericType
                        && i.GetGenericTypeDefinition() != typeof(IDto<>)
                        && dtoContract.IsAssignableFrom(i)
                    );

                foreach (Type dtoInterface in dtoInterfaces)
                {
                    // Ex. : IDtoMapper<Article, IResponseDto<Article>> pointe vers
                    // StockArticleResponseDtoMapper. Si deux mappers partagent la même
                    // interface DTO, le dernier enregistré l'emporte.
                    services.AddSingleton(
                        typeof(IDtoMapper<,>).MakeGenericType(args[0], dtoInterface),
                        sp => sp.GetRequiredService(mapperType)
                    );
                }
            }
        }

        return services;
    }

    private static bool IsMapper(Type type)
    {
        Type[] mapperTypes = [typeof(IEntityMapper<,>), typeof(IDtoMapper<,>)];

        if (!type.IsGenericType)
            return false;

        Type typeDef = type.GetGenericTypeDefinition();
        return mapperTypes.Contains(typeDef);
    }
}
