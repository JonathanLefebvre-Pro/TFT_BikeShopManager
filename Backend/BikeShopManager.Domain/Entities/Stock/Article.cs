namespace BikeShopManager.Domain.Entities.Stock;

public class Article : Audit
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BrandId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public decimal PurchasePriceExclVat { get; set; }
    public decimal SalePriceInclVat { get; set; }
    public int VatRateId { get; set; }
    public int AvailableQuantity { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public string? EAN { get; set; }
    public bool IsActive { get; set; } = true;
}
