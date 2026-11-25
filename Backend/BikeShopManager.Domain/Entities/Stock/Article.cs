using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.Domain.Entities.Stock;

public class Article : StockEntityBase, IActivable
{
    public required string Name { get; set; } = string.Empty;
    public required int BrandId { get; set; }
    public string? Description { get; set; } = string.Empty;
    public required int CategoryId { get; set; }
    public required decimal PurchasePriceExclVat { get; set; }
    public required decimal SalePriceInclVat { get; set; }
    public required int VatRateId { get; set; }
    public required int AvailableQuantity { get; set; }
    public required int MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public string? EAN { get; set; }
    public bool IsActive { get; set; } = true;
}
