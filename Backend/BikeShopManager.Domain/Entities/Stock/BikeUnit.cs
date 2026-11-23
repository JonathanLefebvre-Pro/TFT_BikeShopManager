namespace BikeShopManager.Domain.Entities.Stock;

public class BikeUnit : Audit
{
    public int Id { get; set; }
    public required int BikeModelId { get; set; }
    public required string SerialNumber { get; set; } = string.Empty;
    public required int LocationId { get; set; }
    public string Status { get; set; } = "InStock"; //InStock, Reserved or Sold (For validation)
}
