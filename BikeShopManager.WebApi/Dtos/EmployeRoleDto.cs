using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class EmployeRoleDto
{
    [Required]
    public required string Name { get; set; }
}
