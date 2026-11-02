using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class EmployeeUpdateDto
{
    #region Employee
    //[Required]
    //public required string Password { get; set; }

    //[Required]
    //public required string SecurityStamp { get; set; }

    //public int AccessFailedCount { get; set; }
    //public DateTimeOffset LockoutEnd { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public required DateOnly HiredAt { get; set; }
    public DateOnly? ContractEndAt { get; set; }
    #endregion

    #region User
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }

    [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid ZipCode")]
    public string? ZipCode { get; set; }
    public string? Country { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? BirthDate { get; set; }

    [Required]
    public bool AllowMarketing { get; set; }
    #endregion

    #region Role
    [Required]
    public required int RoleId { get; set; }
    #endregion
}
