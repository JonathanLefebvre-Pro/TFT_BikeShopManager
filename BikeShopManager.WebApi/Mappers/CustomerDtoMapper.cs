using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;

namespace BikeShopManager.WebApi.Mappers;

public static class CustomerDtoMapper
{
    public static Customer ToEntity(this CustomerDto dto)
    {
        return new Customer() { VatNumber = dto.VatNumber };
    }
}
