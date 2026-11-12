namespace BikeShopManager.Domain.Entities.Stock;

public class Category : Audit
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ParentCategoryId { get; set; }
    public required string Domain { get; set; } = string.Empty; //Article or Bike
}
