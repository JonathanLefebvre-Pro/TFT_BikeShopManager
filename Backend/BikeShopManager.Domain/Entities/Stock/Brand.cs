namespace BikeShopManager.Domain.Entities.Stock;

public class Brand : StockBase
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
}
