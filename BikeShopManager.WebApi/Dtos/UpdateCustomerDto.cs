using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class UpdateCustomerDto
{
    public UserDto? User { get; set; }

    public CustomerDto? Customer { get; set; }
}
