using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.WebApi.Mappers.Stock.Abstraction;

public abstract class StockEntityMapperBase<TEntity, TDto> : IEntityMapper<TEntity, TDto>
    where TEntity : EntityBase
    where TDto : IDto<TEntity>
{
    public abstract TEntity ToEntity(TDto dto);

    public virtual IEnumerable<TEntity> ToEntities(IEnumerable<TDto> dtos)
    {
        List<TEntity> entities = new List<TEntity>();
        foreach (TDto dto in dtos)
        {
            entities.Add(ToEntity(dto));
        }

        return entities;
    }
}
