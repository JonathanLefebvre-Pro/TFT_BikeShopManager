using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class EmployeeRoleDto
{
    [Required]
    public required int Id { get; set; }
}
