using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services.Employees;

public class GetEmployeeByIdQueryHandler : IQueryHandlerAsync<GetEmployeeByIdQuery, Employee>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRoleRepository _employeeRoleRepository;
    private readonly IRoleRepository _roleRepository;

    public GetEmployeeByIdQueryHandler(
        IEmployeeRepository employeeRepository,
        IUserRepository userRepository,
        IEmployeeRoleRepository employeeRoleRepository,
        IRoleRepository roleRepository
    )
    {
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
        _employeeRoleRepository = employeeRoleRepository;
        _roleRepository = roleRepository;
    }

    public async Task<Result<Employee>> HandleAsync(GetEmployeeByIdQuery query)
    {
        Employee? employee = await _employeeRepository.GetEmployeeById(query.Id);

        if (employee is null)
            return Result<Employee>.Failure(EmployeeError.NotFound);

        User? user = await _userRepository.GetUserById(employee.UserId);

        if (user is null)
            return Result<Employee>.Failure(UserError.NotFound);

        employee.User = user;

        //Get employee role
        int roleId = await _employeeRoleRepository.GetEmployeeRole(employee.Id);
        if (roleId is 0)
            return Result<Employee>.Failure(EmployeeRoleError.NotFound);

        Role? role = await _roleRepository.GetRoleById(roleId);
        if (role is null)
            return Result<Employee>.Failure(RoleError.NotFound);

        employee.Role = role;

        return employee;
    }
}
