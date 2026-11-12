namespace BikeShopManager.Domain.Entities.Stock;

public class BikeUnit : Audit
{
    public int Id { get; set; }
    public int BikeModelId { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public string Status { get; set; } = "InStock";
}
