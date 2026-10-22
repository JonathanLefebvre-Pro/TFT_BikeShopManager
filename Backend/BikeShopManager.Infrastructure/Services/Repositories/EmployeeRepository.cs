using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class EmployeeRepository(IDbSession DbSession) : IEmployeeRepository
{
    private const string _tableName = @"Persons.Employees";

    public async Task<int> CreateEmployee(int userId, Employee employee)
    {
        string EmployeeSqlQuery =
            @$"INSERT INTO {_tableName} (UserId, PasswordHash, SecurityStamp, HiredAt, ContractEndAt) 
                    OUTPUT INSERTED.ID 
                    VALUES (@UserId, @PasswordHash, @SecurityStamp, @HiredAt, @ContractEndAt)";
        DynamicParameters employeeParameters = new DynamicParameters();
        employeeParameters.Add("@UserId", userId);
        employeeParameters.Add("@PasswordHash", employee.PasswordHash);
        employeeParameters.Add("@SecurityStamp", employee.SecurityStamp);
        employeeParameters.Add("@HiredAt", employee.HiredAt);
        employeeParameters.Add("@ContractEndAt", employee.ContractEndAt);

        return await DbSession.Connection.ExecuteScalarAsync<int>(
            EmployeeSqlQuery,
            employeeParameters,
            DbSession.Transaction
        );
    }

    public async Task<bool> DisableEmployee(int id)
    {
        string sqlQuery =
            @$"UPDATE {_tableName} 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE {_tableName}.Id = @Id
                AND {_tableName}.IsActive='true'";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        parameters.Add("@IsActive", false);
        parameters.Add("@UpdatedAt", DateTime.UtcNow);

        int row = await DbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );

        return row is not 0;
    }

    public async Task<bool> EnableEmployee(int id)
    {
        string sqlQuery =
            @$"UPDATE {_tableName} 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE {_tableName}.Id = @Id 
                AND {_tableName}.IsActive='false'";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        parameters.Add("@IsActive", true);
        parameters.Add("@UpdatedAt", DateTime.UtcNow);

        int row = await DbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );

        return row is not 0;
    }

    public async Task<int> GetEmployeeUserId(int id)
    {
        string employeeSqlQuery = @$"SELECT UserId FROM {_tableName} WHERE {_tableName}.Id=@Id";

        DynamicParameters employeeParameters = new DynamicParameters();
        employeeParameters.Add("@Id", id);

        return await DbSession.Connection.QuerySingleOrDefaultAsync<int>(
            employeeSqlQuery,
            employeeParameters,
            DbSession.Transaction
        );
    }

    public async Task<Employee?> GetEmployeeById(int id)
    {
        string employeeSqlQuery =
            @$"SELECT Id, UserId, PasswordHash, SecurityStamp, AccessFailedCount, LockoutEnd, HiredAt, ContractEndAt, IsActive, CreatedAt, UpdatedAt
                FROM {_tableName} 
                WHERE {_tableName}.Id=@Id";

        DynamicParameters employeeParameters = new DynamicParameters();
        employeeParameters.Add("@Id", id);

        return await DbSession.Connection.QuerySingleOrDefaultAsync<Employee>(
            employeeSqlQuery,
            employeeParameters,
            DbSession.Transaction
        );
    }

    public async Task<Employee?> GetEmployeeByUserId(int id)
    {
        string employeesSqlQuery =
            @$"SELECT Id, UserId, PasswordHash, SecurityStamp, AccessFailedCount, LockoutEnd, HiredAt, ContractEndAt, IsActive, CreatedAt, UpdatedAt 
                FROM {_tableName} 
                WHERE {_tableName}.UserId=@Id";

        DynamicParameters employeesParameters = new DynamicParameters();
        employeesParameters.Add("@Id", id);

        return await DbSession.Connection.QuerySingleOrDefaultAsync<Employee>(
            employeesSqlQuery,
            employeesParameters,
            DbSession.Transaction
        );
    }

    public async Task<IEnumerable<Employee>> GetEmployees(int offset, int limit)
    {
        string employeesSqlQuery =
            @$"SELECT Id, UserId, PasswordHash, SecurityStamp, AccessFailedCount, LockoutEnd, HiredAt, ContractEndAt, IsActive, CreatedAt, UpdatedAt  
                FROM {_tableName}
                ORDER BY Id
                OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";

        DynamicParameters employeesParameters = new DynamicParameters();
        employeesParameters.Add("@Offset", offset);
        employeesParameters.Add("@Limit", limit);

        return await DbSession.Connection.QueryAsync<Employee>(
            employeesSqlQuery,
            employeesParameters,
            DbSession.Transaction
        );
    }

    public async Task<int> GetEmployeesCount()
    {
        string employeesSqlQuery = @$"SELECT COUNT(*) FROM {_tableName}";
        return await DbSession.Connection.ExecuteScalarAsync<int>(
            employeesSqlQuery,
            null,
            DbSession.Transaction
        );
    }

    public async Task<bool> IsEmployeeExist(int id)
    {
        string employeeSqlQuery = @$"SELECT Id FROM {_tableName} WHERE {_tableName}.Id=@Id";

        DynamicParameters employeeParameters = new DynamicParameters();
        employeeParameters.Add("@Id", id);

        int employeeId = await DbSession.Connection.QuerySingleOrDefaultAsync<int>(
            employeeSqlQuery,
            employeeParameters,
            DbSession.Transaction
        );

        return employeeId is not 0;
    }

    public async Task<bool> UpdateEmployee(int id, Employee employee)
    {
        string employeeSqlQuery =
            @$"UPDATE {_tableName} 
                SET HiredAt=@HiredAt, ContractEndAt=@ContractEndAt, UpdatedAt=@UpdatedAt
                WHERE {_tableName}.Id=@Id";
        DynamicParameters employeeParameters = new DynamicParameters();
        employeeParameters.Add("@Id", id);
        employeeParameters.Add("@HiredAt", employee.HiredAt);
        employeeParameters.Add("@ContractEndAt", employee.ContractEndAt);
        employeeParameters.Add("@UpdatedAt", DateTime.UtcNow);

        int rows = await DbSession.Connection.ExecuteAsync(
            employeeSqlQuery,
            employeeParameters,
            DbSession.Transaction
        );

        return rows is not 0;
    }
}
