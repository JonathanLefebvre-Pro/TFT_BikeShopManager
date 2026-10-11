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

    public GetEmployeeByIdQueryHandler(
        IEmployeeRepository employeeRepository,
        IUserRepository userRepository
    )
    {
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
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
        return employee;
    }
}
