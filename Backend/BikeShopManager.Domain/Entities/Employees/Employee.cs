using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.Domain.Entities;

public class Employee : EntityBase, IAudit
{
    public int UserId { get; set; }
    public string Password { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string SecurityStamp { get; set; } = default!;
    public int AccessFailedCount { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public DateOnly HiredAt { get; set; }
    public DateOnly? ContractEndAt { get; set; }
    public bool IsActive { get; set; } = true;

    public User User { get; set; } = default!;
    public Role Role { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
