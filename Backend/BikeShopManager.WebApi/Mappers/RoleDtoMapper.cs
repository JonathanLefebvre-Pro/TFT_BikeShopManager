using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;

namespace BikeShopManager.WebApi.Mappers;

public static class RoleDtoMapper
{
    public static RoleReturnDto ToRoleReturnDto(this Role entity)
    {
        return new RoleReturnDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            NormalizedName = entity.NormalizedName,
        };
    }
}
