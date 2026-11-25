using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.Domain.Entities.Stock;

public class BikeModel : StockEntityBase, IActivable
{
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
