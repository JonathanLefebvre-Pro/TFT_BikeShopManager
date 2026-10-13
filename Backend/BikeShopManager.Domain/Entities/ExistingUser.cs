namespace BikeShopManager.Domain.Entities;

public class ExistingUser
{
    public int Id { get; set; } = 0;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Email { get; set; }
    public DateOnly? BirthDate { get; set; }
}
