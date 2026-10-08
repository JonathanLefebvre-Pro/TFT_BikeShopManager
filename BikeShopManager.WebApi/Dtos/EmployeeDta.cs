namespace BikeShopManager.WebApi.Dtos;

public class EmployeeDto
{
    //public string PasswordHash { get; set; } = default!;
    //public string SecurityStamp { get; set; } = default!;
    //public int AccessFailedCount { get; set; }
    //public DateTimeOffset LockoutEnd { get; set; }
    public DateOnly HiredAt { get; set; }
    public DateOnly ContractEndAt { get; set; }
}
