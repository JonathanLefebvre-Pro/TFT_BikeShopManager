namespace BikeShopManager.Domain.Entities.Stock;

public class Location : StockBase
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
}
