using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.Domain.Entities;

public class Customer : EntityBase, IAudit
{
    public int UserId { get; set; }
    public string? VatNumber { get; set; }
    public bool IsActive { get; set; }

    public User User { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
