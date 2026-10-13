namespace BikeShopManager.Domain.Entities;

public class EmployeeRole : Audit
{
    public int EmployeeId { get; set; }
    public int RoleId { get; set; }
}
