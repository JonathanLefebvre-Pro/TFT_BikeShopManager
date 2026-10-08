using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class AddEmployeeDto
{
    [Required]
    public UserDto User { get; set; } = default!;

    public EmployeeDto Employee { get; set; } = default!;
}
