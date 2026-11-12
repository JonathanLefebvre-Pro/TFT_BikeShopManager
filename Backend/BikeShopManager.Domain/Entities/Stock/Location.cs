namespace BikeShopManager.Domain.Entities.Stock;

public class Location : Audit
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
