using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.WebApi.Mappers.Stock.Abstraction;

public abstract class StockDtoMapperBase<TEntity, TDto> : IDtoMapper<TEntity, TDto>
    where TEntity : EntityBase
    where TDto : IDto<TEntity>
{
    public abstract TDto ToDto(TEntity entity);

    public virtual IEnumerable<TDto> ToDtos(IEnumerable<TEntity> entities)
    {
        List<TDto> dtos = new List<TDto>();
        foreach (TEntity entity in entities)
        {
            dtos.Add(ToDto(entity));
        }

        return dtos;
    }
}
