namespace BikeShopManager.Domain.Entities.Shared;

public interface IAudit
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
