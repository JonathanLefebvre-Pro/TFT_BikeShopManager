namespace BikeShopManager.Domain.Entities.Stock;

public class VatRate : StockBase
{
    public int Id { get; set; }
    public required string Label { get; set; } = string.Empty;
    public required decimal Rate { get; set; }
}
