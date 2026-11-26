namespace BikeShopManager.WebApi.Dtos.Stock;

public class StockAddArticleDto
{
    public required string Name { get; set; }
    public required int BrandId { get; set; }
    public string? Description { get; set; }
    public required int CategoryId { get; set; }
    public required decimal PurchasePriceExclVat { get; set; }
    public required decimal SalePriceInclVat { get; set; }
    public required int VatRateId { get; set; }
    public required int AvailableQuantity { get; set; }
    public required int MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public string? EAN { get; set; }
}
