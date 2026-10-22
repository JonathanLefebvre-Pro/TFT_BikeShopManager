using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos;

public class CustomerAddDto
{
    #region Customer
    public string? VatNumber { get; set; }
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
}
