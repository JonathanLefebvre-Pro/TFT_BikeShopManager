namespace BikeShopManager.WebApi.Dtos;

public class AddEmployeeDto
{
    public UserDto User { get; set; } = default!;

    public EmployeeDto Employee { get; set; } = default!;

    public EmployeeRoleDto EmployeeRole { get; set; } = default!;
}
