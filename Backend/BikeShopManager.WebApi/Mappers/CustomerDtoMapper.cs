using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;

namespace BikeShopManager.WebApi.Mappers;

public static class CustomerDtoMapper
{
    public static CustomerReturnDto ToCustomerReturnDto(this Customer entity)
    {
        return new CustomerReturnDto()
        {
            CustomerId = entity.Id,
            VatNumber = entity.VatNumber,
            IsActive = entity.IsActive,
            UserId = entity.UserId,
            FirstName = entity.User.FirstName,
            LastName = entity.User.LastName,
            Email = entity.User.Email,
            PhoneNumber = entity.User.PhoneNumber,
            Address = entity.User.Address,
            City = entity.User.City,
            ZipCode = entity.User.ZipCode,
            Country = entity.User.Country,
            BirthDate = entity.User.BirthDate,
            AllowMarketing = entity.User.AllowMarketing,
        };
    }

    public static Customer ToCustomerEntity(this CustomerUpdateDto dto)
    {
        return new Customer() { VatNumber = dto.VatNumber };
    }

    public static User ToUserEntity(this CustomerUpdateDto dto)
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

    public static Customer ToCustomerEntity(this CustomerAddDto dto)
    {
        return new Customer() { VatNumber = dto.VatNumber };
    }

    public static User ToUserEntity(this CustomerAddDto dto)
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
