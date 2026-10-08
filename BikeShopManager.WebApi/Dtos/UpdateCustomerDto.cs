using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class UpdateCustomerDto
{
    [Required]
    public UserDto User { get; set; } = default!;

    public CustomerDto Customer { get; set; } = default!;
}
