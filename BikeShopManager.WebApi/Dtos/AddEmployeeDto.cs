namespace BikeShopManager.WebApi.Dtos;

public class AddEmployeeDto
{
    public UserDto User { get; set; } = default!;

    public EmployeeDto Employee { get; set; } = default!;

    public EmployeRoleDto EmployeRole { get; set; } = default!;
}
