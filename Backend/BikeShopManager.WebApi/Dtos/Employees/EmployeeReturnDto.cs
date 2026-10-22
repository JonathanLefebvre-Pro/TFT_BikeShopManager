namespace BikeShopManager.WebApi.Dtos;

public class EmployeeReturnDto
{
    #region Employee
    public required string PasswordHash { get; set; }
    public required string SecurityStamp { get; set; }

    //public int AccessFailedCount { get; set; }
    //public DateTimeOffset LockoutEnd { get; set; }
    public required DateOnly HiredAt { get; set; }
    public DateOnly? ContractEndAt { get; set; }
    public bool IsActive { get; set; } = true;
    #endregion

    #region User
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

    #region Role
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string NormalizedRoleName { get; set; }
    #endregion
}
