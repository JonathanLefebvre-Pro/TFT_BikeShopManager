using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class AddCustomerDto
{
    [Required]
    public required UserDto User { get; set; }

    public CustomerDto Customer { get; set; } = default!;
}
