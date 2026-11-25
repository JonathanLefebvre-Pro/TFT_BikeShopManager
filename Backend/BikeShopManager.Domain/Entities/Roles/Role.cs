using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.Domain.Entities;

public class Role : IAudit
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string NormalizedName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
