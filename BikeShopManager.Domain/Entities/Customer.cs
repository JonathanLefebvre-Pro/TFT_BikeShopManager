namespace BikeShopManager.Domain.Entities;

public class Customer : Audit
{
    public int Id { get; set; } = 0;
    public int UserId { get; set; }
    public string VatNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public User User { get; set; } = default!;
}
