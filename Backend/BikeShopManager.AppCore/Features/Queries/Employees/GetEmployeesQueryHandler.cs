using System.Text.RegularExpressions;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Employees;

public class GetEmployeesQueryHandler
    : IQueryHandlerAsync<GetEmployeesQuery, PageResponse<Employee>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRoleRepository _employeeRoleRepository;
    private readonly IRoleRepository _roleRepository;

    public GetEmployeesQueryHandler(
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

    public async Task<Result<PageResponse<Employee>>> HandleAsync(GetEmployeesQuery query)
    {
        int pageNumber = Math.Max(1, query.PageNumber);
        int pageSize = Math.Clamp(query.PageSize, 1, 50);

        int offset = (pageNumber - 1) * pageSize;
        int limit = pageSize;

        //Is search keywords used or not ?
        if (query.Keywords == null || query.Keywords.Length is 0)
            return await GetWithoutSearch(offset, limit, pageSize, pageNumber);
        else
            return await GetWithSearch(query.Keywords, offset, limit, pageSize, pageNumber);
    }

    private async Task<Result<PageResponse<Employee>>> GetWithSearch(
        string keywordString,
        int offset,
        int limit,
        int pageSize,
        int pageNumber
    )
    {
        string keyword = Regex.Replace(keywordString.Trim(), @"\s+", " ");
        string[] keywords = keyword.Split(' ');

        int totalRecords = await _userRepository.GetUsersCount(keywords);
        IEnumerable<User> users = await _userRepository.GetUsers(offset, limit, keywords);
        if (users is null || users.Count() <= 0)
            return Result<PageResponse<Employee>>.Failure(UserError.NotFound);

        //For each users, get his employee
        List<Employee> employees = new List<Employee>();
        foreach (User user in users)
        {
            Employee? employee = await _employeeRepository.GetEmployeeByUserId(user.Id);
            if (employee is not null)
            {
                employee.User = user;

                //Get employee role
                int roleId = await _employeeRoleRepository.GetEmployeeRole(employee.Id);
                if (roleId is 0)
                    return Result<PageResponse<Employee>>.Failure(EmployeeRoleError.NotFound);

                Role? role = await _roleRepository.GetRoleById(roleId);
                if (role is null)
                    return Result<PageResponse<Employee>>.Failure(RoleError.NotFound);

                employee.Role = role;

                employees.Add(employee);
            }
        }

        if (employees.Count() is 0)
            return Result<PageResponse<Employee>>.Failure(EmployeeError.NotFound);

        PageResponse<Employee> response = new PageResponse<Employee>()
        {
            Data = employees,
            PageSize = pageSize,
            PageNumber = pageNumber,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize),
        };

        return Result<PageResponse<Employee>>.Success(response);
    }

    private async Task<Result<PageResponse<Employee>>> GetWithoutSearch(
        int offset,
        int limit,
        int pageSize,
        int pageNumber
    )
    {
        int totalRecords = await _employeeRepository.GetEmployeesCount();
        IEnumerable<Employee> employees = await _employeeRepository.GetEmployees(offset, limit);

        if (employees is null || employees.Count() <= 0)
            return Result<PageResponse<Employee>>.Failure(EmployeeError.NotFound);

        foreach (Employee employee in employees)
        {
            //TODO : Cancel query if one employee has no user ???? This could be a problem - other solution ?
            User? user = await _userRepository.GetUserById(employee.UserId);
            //if (user is null)
            //return Result<IEnumerable<Employee>>.Failure(UserError.NotFound);
            if (user is not null)
                employee.User = user;

            //Get employee role
            int roleId = await _employeeRoleRepository.GetEmployeeRole(employee.Id);
            if (roleId is 0)
                return Result<PageResponse<Employee>>.Failure(EmployeeRoleError.NotFound);

            Role? role = await _roleRepository.GetRoleById(roleId);
            if (role is null)
                return Result<PageResponse<Employee>>.Failure(RoleError.NotFound);

            employee.Role = role;
        }

        PageResponse<Employee> response = new PageResponse<Employee>()
        {
            Data = employees,
            PageSize = pageSize,
            PageNumber = pageNumber,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize),
        };

        return Result<PageResponse<Employee>>.Success(response);
    }
}
