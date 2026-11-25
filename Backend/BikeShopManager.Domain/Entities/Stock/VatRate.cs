namespace BikeShopManager.Domain.Entities.Stock;

public class VatRate : StockEntityBase
{
    public required string Label { get; set; } = string.Empty;
    public required decimal Rate { get; set; }
}
