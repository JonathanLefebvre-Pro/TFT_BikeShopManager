namespace BikeShopManager.Domain.Entities;

public class Employee : Audit
{
    public int Id { get; set; } = 0;
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
}
