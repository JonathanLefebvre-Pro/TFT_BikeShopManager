namespace BikeShopManager.Domain.Entities.Stock;

public class Category : StockEntityBase
{
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ParentCategoryId { get; set; }
    public required string Domain { get; set; } = string.Empty; //Article or Bike (For validation)
}
