using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployees(int offset, int limit);
    Task<int> CreateEmployee(int userId, Employee employee);
    Task<Employee?> GetEmployeeById(int id);
    Task<Employee?> GetEmployeeByUserId(int id);
    Task<int> GetEmployeeUserId(int id);
    Task<int> GetEmployeesCount();
    Task<bool> DisableEmployee(int id);
    Task<bool> EnableEmployee(int id);
    Task<bool> UpdateEmployee(int id, Employee employee);
    Task<bool> UpdateEmployeePassword(int id, string passwordHash, string securityStamp);
    Task<bool> IsEmployeeExist(int id);
    Task<bool> UpdateEmployeeSecurityStamp(int id, string securityStamp);
}
