namespace BikeShopManager.Domain.Entities;

public class User : Audit
{
    public int Id { get; set; } = 0;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public DateOnly? BirthDate { get; set; }
    public bool AllowMarketing { get; set; }
}
