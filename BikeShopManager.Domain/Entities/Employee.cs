namespace BikeShopManager.Domain.Entities;

public class Employee : Audit
{
    public int Id { get; set; } = 0;
    public int UserId { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string SecurityStamp { get; set; } = string.Empty;
    public int AccessFailedCount { get; set; } = 0;
    public DateTimeOffset LockoutEnd { get; set; }
    public DateTime HiredAt { get; set; }
    public DateTime ContractEndAt { get; set; }
    public bool IsActive { get; set; } = true;
}
