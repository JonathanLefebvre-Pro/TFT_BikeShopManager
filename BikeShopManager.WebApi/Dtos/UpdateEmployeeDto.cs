using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class UpdateEmployeeDto
{
    public UserDto? User { get; set; }

    public EmployeeDto? Employee { get; set; }
}
