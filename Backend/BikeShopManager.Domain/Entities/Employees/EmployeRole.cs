using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.Domain.Entities;

public class EmployeeRole : IAudit
{
    public int EmployeeId { get; set; }
    public int RoleId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
