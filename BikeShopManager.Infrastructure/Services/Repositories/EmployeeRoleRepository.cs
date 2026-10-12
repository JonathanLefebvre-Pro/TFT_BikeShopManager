using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class EmployeeRoleRepository(IDbSession DbSession) : IEmployeeRoleRepository
{
    private const string _tableName = @"Persons.EmployeeRoles";

    public async Task<bool> CreateEmployeeRole(int employeeId, int roleId)
    {
        string sqlQuery =
            @$"INSERT INTO {_tableName} (EmployeeId, RoleId) VALUES (@EmployeeId, @RoleId)";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", employeeId);
        parameters.Add("@RoleId", roleId);

        int rows = await DbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );

        return rows is not 0;
    }

    public async Task<int> GetEmployeeRole(int employeeId)
    {
        string sqlQuery = @$"SELECT RoleId FROM {_tableName} WHERE EmployeeId=@EmployeeId";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", employeeId);

        return await DbSession.Connection.QuerySingleOrDefaultAsync<int>(sqlQuery, parameters);
    }

    public async Task<IEnumerable<int>> GetEmployeesByRole(int roleId)
    {
        string sqlQuery = @$"SELECT EmployeeId FROM {_tableName} WHERE RoleId=@RoleId";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@RoleId", roleId);

        return await DbSession.Connection.QueryAsync<int>(sqlQuery, parameters);
    }
}
