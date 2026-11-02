namespace BikeShopManager.Domain.Entities;

public class Role : Audit
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string NormalizedName { get; set; }
}
