using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.Domain.Entities;

public class Audit : IAudit
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
