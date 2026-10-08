using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class EmployeeDto
{
    [Required]
    public required string PasswordHash { get; set; }

    [Required]
    public required string SecurityStamp { get; set; }

    //public int AccessFailedCount { get; set; }
    //public DateTimeOffset LockoutEnd { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateOnly HiredAt { get; set; }
    public DateOnly? ContractEndAt { get; set; }
}
