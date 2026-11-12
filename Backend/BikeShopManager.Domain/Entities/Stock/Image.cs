namespace BikeShopManager.Domain.Entities.Stock;

public class Image : Audit
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public int BikeModelId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
}
