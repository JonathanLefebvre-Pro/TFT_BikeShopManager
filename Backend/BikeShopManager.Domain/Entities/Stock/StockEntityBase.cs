using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.Domain.Entities.Stock;

public abstract class StockEntityBase : EntityBase, IAudit
{
    public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
