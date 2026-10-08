using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;

namespace BikeShopManager.WebApi.Mappers;

public static class EmployeeDtoMapper
{
    public static Employee ToEntity(this EmployeeDto dto)
    {
        return new Employee()
        {
            //PasswordHash = dto.PasswordHash,
            //SecurityStamp = dto.SecurityStamp,
            //AccessFailedCount = dto.AccessFailedCount,
            //LockoutEnd = dto.LockoutEnd,
            HiredAt = dto.HiredAt,
            ContractEndAt = dto.ContractEndAt,
        };
    }
}
