namespace BikeShopManager.Domain.Entities.Stock;

public class Category : Audit
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ParentCategoryId { get; set; }
    public string Domain { get; set; } = string.Empty;
}
