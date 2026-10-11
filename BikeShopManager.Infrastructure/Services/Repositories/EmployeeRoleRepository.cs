using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class EmployeeRoleRepository : IEmployeeRoleRepository
{
    private readonly IDbSession _dbSession;

    public EmployeeRoleRepository(IDbSession dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<bool> CreateEmployeeRole(int employeeId, int roleId)
    {
        string insertRoleSqlQuery =
            @"INSERT INTO Persons.EmployeeRoles (EmployeeId, RoleId) VALUES (@EmployeeId, @RoleId)";
        DynamicParameters insertRoleParameters = new DynamicParameters();
        insertRoleParameters.Add("@EmployeeId", employeeId);
        insertRoleParameters.Add("@RoleId", roleId);

        int rows = await _dbSession.Connection.ExecuteAsync(
            insertRoleSqlQuery,
            insertRoleParameters,
            _dbSession.Transaction
        );

        return rows is not 0;
    }
}
