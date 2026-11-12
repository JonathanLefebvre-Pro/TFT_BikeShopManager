namespace BikeShopManager.Domain.Entities.Stock;

public class BikeModel : Audit
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BrandId { get; set; }
    public string Model { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public decimal PurchasePriceExclVat { get; set; }
    public decimal SalePriceInclVat { get; set; }
    public int VatRateId { get; set; }
    public string? EAN { get; set; }
    public bool IsActive { get; set; } = true;
}
