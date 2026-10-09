using System.Data;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Employees;

public class GetEmployeeByIdQueryHandler : IQueryHandlerAsync<GetEmployeeByIdQuery, Employee>
{
    private readonly IDbConnection _dbConnection;

    public GetEmployeeByIdQueryHandler(IDbConnection DbConnection)
    {
        _dbConnection = DbConnection;
        _dbConnection.Open();
    }

    public async Task<Result<Employee>> HandleAsync(
        GetEmployeeByIdQuery query,
        IDbTransaction transact = null!
    )
    {
        string employeeSqlQuery =
            @"SELECT Id, UserId, PasswordHash, SecurityStamp, AccessFailedCount, LockoutEnd, HiredAt, ContractEndAt, IsActive, CreatedAt, UpdatedAt
                FROM Persons.Employees 
                WHERE Persons.Employees.Id=@Id";

        DynamicParameters employeeParameters = new DynamicParameters();
        employeeParameters.Add("@Id", query.Id);

        Employee? employee = await _dbConnection.QuerySingleOrDefaultAsync<Employee>(
            employeeSqlQuery,
            employeeParameters,
            transact
        );

        if (employee is null)
            return Result<Employee>.Failure(EmployeeError.NotFound);

        string userSqlQuery =
            @"SELECT Id, FirstName, LastName , Email , PhoneNumber , Address , City , ZipCode , Country , BirthDate , AllowMarketing, CreatedAt, UpdatedAt
                FROM Persons.Users WHERE Persons.Users.Id = @UserId";
        DynamicParameters userParameters = new DynamicParameters();
        userParameters.Add("@UserId", employee.UserId);

        User? user = await _dbConnection.QuerySingleOrDefaultAsync<User>(
            userSqlQuery,
            userParameters,
            transact
        );

        if (user is null)
            return Result<Employee>.Failure(EmployeeError.NotFound);

        employee.User = user;
        return employee;
    }
}
