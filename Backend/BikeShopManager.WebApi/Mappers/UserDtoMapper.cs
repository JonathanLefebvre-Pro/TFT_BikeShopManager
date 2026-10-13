using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;

namespace BikeShopManager.WebApi.Mappers;

public static class UserDtoMapper
{
    public static User ToEntity(this UserDto dto)
    {
        return new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            City = dto.City,
            ZipCode = dto.ZipCode,
            Country = dto.Country,
            BirthDate = dto.BirthDate,
            AllowMarketing = dto.AllowMarketing,
        };
    }
}
