namespace BikeShopManager.Domain.Entities;

public class User : Audit
{
    public int Id { get; set; } = 0;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string City { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string Country { get; set; } = default!;
    public DateOnly BirthDate { get; set; }
    public bool AllowMarketing { get; set; }
}
