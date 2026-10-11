using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbSession _dbSession;

    public EmployeeRepository(IDbSession dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<int> CreateEmployee(int userId, Employee employee)
    {
        string EmployeeSqlQuery =
            @"INSERT INTO Persons.Employees (UserId, PasswordHash, SecurityStamp, HiredAt, ContractEndAt) 
                    OUTPUT INSERTED.ID 
                    VALUES (@UserId, @PasswordHash, @SecurityStamp, @HiredAt, @ContractEndAt)";
        DynamicParameters employeeParameters = new DynamicParameters();
        employeeParameters.Add("@UserId", userId);
        employeeParameters.Add("@PasswordHash", employee.PasswordHash);
        employeeParameters.Add("@SecurityStamp", employee.SecurityStamp);
        employeeParameters.Add("@HiredAt", employee.HiredAt);
        employeeParameters.Add("@ContractEndAt", employee.ContractEndAt);

        return await _dbSession.Connection.ExecuteScalarAsync<int>(
            EmployeeSqlQuery,
            employeeParameters,
            _dbSession.Transaction
        );
    }

    public async Task<bool> DisableEmployee(int id)
    {
        string sqlQuery =
            @"UPDATE Persons.Employees 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE Persons.Employees.Id = @Id
                AND Persons.Employees.IsActive='true'";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        parameters.Add("@IsActive", false);
        parameters.Add("@UpdatedAt", DateTime.UtcNow);

        int row = await _dbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );

        return row is not 0;
    }

    public async Task<bool> EnableEmployee(int id)
    {
        string sqlQuery =
            @"UPDATE Persons.Employees 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE Persons.Employees.Id = @Id 
                AND Persons.Employees.IsActive='false'";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        parameters.Add("@IsActive", true);
        parameters.Add("@UpdatedAt", DateTime.UtcNow);

        int row = await _dbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );

        return row is not 0;
    }

    public async Task<Employee?> GetEmployeeById(int id)
    {
        string employeeSqlQuery =
            @"SELECT Id, UserId, PasswordHash, SecurityStamp, AccessFailedCount, LockoutEnd, HiredAt, ContractEndAt, IsActive, CreatedAt, UpdatedAt
                FROM Persons.Employees 
                WHERE Persons.Employees.Id=@Id";

        DynamicParameters employeeParameters = new DynamicParameters();
        employeeParameters.Add("@Id", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Employee>(
            employeeSqlQuery,
            employeeParameters,
            _dbSession.Transaction
        );
    }

    public async Task<bool> UpdateEmployee(int id, Employee employee)
    {
        string employeeSqlQuery =
            @"UPDATE Persons.Employees 
                SET PasswordHash=@PasswordHash, SecurityStamp=@SecurityStamp, 
                HiredAt=@HiredAt, ContractEndAt=@ContractEndAt, UpdatedAt=@UpdatedAt
                WHERE Persons.Employees.Id=@Id";
        DynamicParameters employeeParameters = new DynamicParameters();
        employeeParameters.Add("@Id", id);
        employeeParameters.Add("@PasswordHash", employee.PasswordHash);
        employeeParameters.Add("@SecurityStamp", employee.SecurityStamp);
        employeeParameters.Add("@HiredAt", employee.HiredAt);
        employeeParameters.Add("@ContractEndAt", employee.ContractEndAt);
        employeeParameters.Add("@UpdatedAt", DateTime.UtcNow);

        int rows = await _dbSession.Connection.ExecuteAsync(
            employeeSqlQuery,
            employeeParameters,
            _dbSession.Transaction
        );

        return rows is not 0;
    }
}
