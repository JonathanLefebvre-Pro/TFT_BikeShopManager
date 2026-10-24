namespace BikeShopManager.WebApi.Dtos;

public class CustomerResponseDto
{
    #region Customer
    public int CustomerId { get; set; }
    public string? VatNumber { get; set; }
    public bool IsActive { get; set; }
    #endregion

    #region User
    public int UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public DateOnly? BirthDate { get; set; }
    public bool AllowMarketing { get; set; }
    #endregion
}
