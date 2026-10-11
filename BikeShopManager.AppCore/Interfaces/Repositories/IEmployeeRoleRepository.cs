using System;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IEmployeeRoleRepository
{
    Task<bool> CreateEmployeeRole(int employeeId, int roleId);
}
