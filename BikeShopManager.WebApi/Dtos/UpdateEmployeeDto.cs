using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class UpdateEmployeeDto
{
    [Required]
    public UserDto User { get; set; } = default!;

    public EmployeeDto Employee { get; set; } = default!;
}
