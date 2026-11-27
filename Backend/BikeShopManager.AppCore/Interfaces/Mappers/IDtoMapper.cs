using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.AppCore.Interfaces.Mappers;

public interface IDtoMapper<TEntity, TDto>
    where TEntity : EntityBase
    where TDto : IDto<TEntity>
{
    TDto ToDto(TEntity entity);
    IEnumerable<TDto> ToDtos(IEnumerable<TEntity> entities);
}
