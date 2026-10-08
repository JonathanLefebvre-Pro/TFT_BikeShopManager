using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class UpdateCustomerDto
{
    [Required]
    public required UserDto User { get; set; }

    [Required]
    public required CustomerDto Customer { get; set; }
}
