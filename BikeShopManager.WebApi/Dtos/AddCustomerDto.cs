using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class AddCustomerDto
{
    [Required]
    public UserDto User { get; set; } = default!;

    public CustomerDto Customer { get; set; } = default!;
}
