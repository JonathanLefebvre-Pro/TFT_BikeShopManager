namespace BikeShopManager.Domain.Entities.Stock;

public class Brand : Audit
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
}
