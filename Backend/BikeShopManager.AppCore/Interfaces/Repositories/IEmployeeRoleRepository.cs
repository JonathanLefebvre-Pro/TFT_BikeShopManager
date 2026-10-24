namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IEmployeeRoleRepository
{
    Task<bool> CreateEmployeeRole(int employeeId, int roleId);
    Task<int> GetEmployeeRole(int employeeId);
    Task<IEnumerable<int>> GetEmployeesByRole(int roleId);
    Task<bool> UpdateEmployeeRole(int employeeId, int roleId);
}
