using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class EmployeeUpdatePasswordDto
{
    #region Employee
    [Required]
    public required string OldPassword { get; set; }

    [Required]
    public required string NewPassword { get; set; }
    #endregion
}
