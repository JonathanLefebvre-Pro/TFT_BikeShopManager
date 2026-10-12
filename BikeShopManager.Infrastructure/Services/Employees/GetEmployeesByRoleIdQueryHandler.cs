using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services.Roles;

public class GetEmployeesByRoleIdQueryHandler(
    IEmployeeRepository EmployeeRepository,
    IEmployeeRoleRepository EmployeeRoleRepository,
    IUserRepository UserRepository,
    IRoleRepository RoleRepository
) : IQueryHandlerAsync<GetEmployeesByRoleIdQuery, IEnumerable<Employee>>
{
    public async Task<Result<IEnumerable<Employee>>> HandleAsync(GetEmployeesByRoleIdQuery query)
    {
        //Check if the role exist
        bool isRoleExist = await RoleRepository.IsRoleExist(query.Id);
        if (!isRoleExist)
            return Result<IEnumerable<Employee>>.Failure(RoleError.NotFound);

        //Get all the employees from the Persons.EmployeeRoles that match the role id
        IEnumerable<int> employeeIds = await EmployeeRoleRepository.GetEmployeesByRole(query.Id);

        if (employeeIds is null || employeeIds.Count() <= 0)
            return Result<IEnumerable<Employee>>.Failure(EmployeeError.NotFound);

        //TODO : create a list of employees based on the list of ids

        List<Employee> employees = new List<Employee>();
        foreach (int id in employeeIds)
        {
            Employee? emp = await EmployeeRepository.GetEmployeeById(id);
            if (emp is not null)
            {
                User? user = await UserRepository.GetUserById(emp.UserId);
                if (user is null)
                    return Result<IEnumerable<Employee>>.Failure(UserError.NotFound);

                emp.User = user;
                employees.Add(emp);
            }
        }

        if (employees is null || employees.Count() <= 0)
            return Result<IEnumerable<Employee>>.Failure(EmployeeError.NotFound);

        return Result<IEnumerable<Employee>>.Success(employees);
    }
}
