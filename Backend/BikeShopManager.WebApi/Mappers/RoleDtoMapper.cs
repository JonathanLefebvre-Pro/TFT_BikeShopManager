using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;

namespace BikeShopManager.WebApi.Mappers;

public static class RoleDtoMapper
{
    public static RoleResponseDto ToRoleResponseDto(this Role entity)
    {
        return new RoleResponseDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            NormalizedName = entity.NormalizedName,
        };
    }
}
