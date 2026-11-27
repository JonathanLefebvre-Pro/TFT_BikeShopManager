using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.AppCore.Interfaces.Mappers;

public interface IEntityMapper<TEntity, TDto>
    where TEntity : EntityBase
    where TDto : IDto<TEntity>
{
    TEntity ToEntity(TDto dto);
    IEnumerable<TEntity> ToEntities(IEnumerable<TDto> dtos);
}
