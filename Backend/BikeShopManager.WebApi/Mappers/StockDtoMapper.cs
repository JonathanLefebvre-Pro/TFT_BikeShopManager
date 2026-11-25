using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Dtos.Stock;

namespace BikeShopManager.WebApi.Mappers;

public interface IStockDtoMapper<E, D>
    where E : StockEntityBase
{
    static abstract E ToEntity(D dto);
    static abstract IEnumerable<E> ToEntities(IEnumerable<D> dtos);
    static abstract D ToDto(E entity);
    static abstract IEnumerable<D> ToDtos(IEnumerable<E> entities);
}

public class StockDtoMapper : IStockDtoMapper<Article, StockAddArticleDto>
{
    public static StockAddArticleDto ToDto(Article entity)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<StockAddArticleDto> ToDtos(IEnumerable<Article> entities)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<Article> ToEntities(IEnumerable<StockAddArticleDto> dtos)
    {
        throw new NotImplementedException();
    }

    public static Article ToEntity(StockAddArticleDto dto)
    {
        throw new NotImplementedException();
    }
}
