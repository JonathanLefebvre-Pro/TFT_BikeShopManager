namespace BikeShopManager.Domain.Entities.Stock;

public class BikeModel : StockBase, IActivable
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required int BrandId { get; set; }
    public required string Model { get; set; } = string.Empty;
    public string? Description { get; set; }
    public required int CategoryId { get; set; }
    public required decimal PurchasePriceExclVat { get; set; }
    public required decimal SalePriceInclVat { get; set; }
    public required int VatRateId { get; set; }
    public string? EAN { get; set; }
    public bool IsActive { get; set; } = true;
}
