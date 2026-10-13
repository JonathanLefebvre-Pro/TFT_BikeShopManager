using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<int> CreateEmployee(int userId, Employee employee);
    Task<Employee?> GetEmployeeById(int id);
    Task<bool> DisableEmployee(int id);
    Task<bool> EnableEmployee(int id);
    Task<bool> UpdateEmployee(int id, Employee employee);
    Task<bool> IsEmployeeExist(int id);
}
