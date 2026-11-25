namespace BikeShopManager.Domain.Entities.Stock;

public class Image : StockEntityBase
{
    public int? ArticleId { get; set; }
    public int? BikeModelId { get; set; }
    public required string Url { get; set; } = string.Empty;
    public required string ImageName { get; set; } = string.Empty;
}
