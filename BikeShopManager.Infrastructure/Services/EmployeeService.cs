using System.Data;
using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services;

public class EmployeeService : IEmployeeRepository
{
    private IDbConnection _dbConnection;

    public EmployeeService(IDbConnection DbConnection)
    {
        _dbConnection = DbConnection;
        _dbConnection.Open();
    }

    public async Task<Result<int>> HandleAsync(
        AddEmployeeCommand command,
        IDbTransaction transact = null!
    )
    {
        using (
            IDbTransaction transaction = transact is not null
                ? transact
                : _dbConnection.BeginTransaction()
        )
        {
            //Insert user data in User table and recover row id
            string userSqlQuery =
                @"INSERT INTO Persons.Users (FirstName, LastName, Email, PhoneNumber, Address, City, ZipCode, Country, BirthDate, AllowMarketing) 
                    OUTPUT INSERTED.ID
                    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Address, @City, @ZipCode, @Country, @BirthDate, @AllowMarketing)";
            DynamicParameters userParameters = new DynamicParameters();
            userParameters.Add("@FirstName", command.User.FirstName);
            userParameters.Add("@LastName", command.User.LastName);
            userParameters.Add("@Email", command.User.Email);
            userParameters.Add("@PhoneNumber", command.User.PhoneNumber);
            userParameters.Add("@Address", command.User.Address);
            userParameters.Add("@City", command.User.City);
            userParameters.Add("@ZipCode", command.User.ZipCode);
            userParameters.Add("@Country", command.User.Country);
            userParameters.Add("@BirthDate", command.User.BirthDate);
            userParameters.Add("@AllowMarketing", command.User.AllowMarketing);

            int userRowId = 0;
            try
            {
                //Avoid crash if query failed (I.E : user with same email already exist !)
                userRowId = await _dbConnection.ExecuteScalarAsync<int>(
                    userSqlQuery,
                    userParameters,
                    transaction
                );
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex;
            }

            if (userRowId is 0)
            {
                transaction.Rollback();
                return Result<int>.Failure(EmployeeError.NotInserted);
            }

            //Insert Employee data in Employee table and recover row id
            string EmployeeSqlQuery =
                @"INSERT INTO Persons.Employees (UserId, HiredAt, ContractEndAt) 
                    OUTPUT INSERTED.ID 
                    VALUES (@UserId, @HiredAt, @ContractEndAt)";
            DynamicParameters employeeParameters = new DynamicParameters();
            employeeParameters.Add("@UserId", userRowId);
            employeeParameters.Add("@HiredAt", command.Employee.HiredAt);
            employeeParameters.Add("@ContractEndAt", command.Employee.ContractEndAt);

            int employeeRowId = 0;
            try
            {
                employeeRowId = await _dbConnection.ExecuteScalarAsync<int>(
                    EmployeeSqlQuery,
                    employeeParameters,
                    transaction
                );
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex;
            }

            if (employeeRowId is 0)
            {
                transaction.Rollback();
                return Result<int>.Failure(EmployeeError.NotInserted);
            }

            transaction.Commit();
            return Result<int>.Success(employeeRowId);
        }
    }

    public async Task<Result> HandleAsync(
        DisableEmployeeCommand command,
        IDbTransaction transact = null!
    )
    {
        string sqlQuery =
            @"UPDATE Persons.Employees 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE Persons.Employees.Id = @Id
                AND Persons.Employees.IsActive='true'";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", command.Id);
        parameters.Add("@IsActive", false);
        parameters.Add("@UpdatedAt", DateTime.UtcNow);

        int row = await _dbConnection.ExecuteAsync(sqlQuery, parameters, transact);
        if (row is 0)
            return Result.Failure(EmployeeError.NotDisabled);

        return Result.Success();
    }

    public async Task<Result> HandleAsync(
        EnableEmployeeCommand command,
        IDbTransaction transact = null!
    )
    {
        string sqlQuery =
            @"UPDATE Persons.Employees 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE Persons.Employees.Id = @Id 
                AND Persons.Employees.IsActive='false'";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", command.Id);
        parameters.Add("@IsActive", true);
        parameters.Add("@UpdatedAt", DateTime.UtcNow);

        int row = await _dbConnection.ExecuteAsync(sqlQuery, parameters, transact);
        if (row is 0)
            return Result.Failure(EmployeeError.NotEnabled);

        return Result.Success();
    }

    public async Task<Result> HandleAsync(
        UpdateEmployeeCommand command,
        IDbTransaction transact = null!
    )
    {
        using (
            IDbTransaction transaction = transact is not null
                ? transact
                : _dbConnection.BeginTransaction()
        )
        {
            //Get the Employee based on the command Id parameter
            //Get the user row linked to the Employee UserId
            Result<Employee> result = await HandleAsync(
                new GetEmployeeByIdQuery(command.Id),
                transaction
            );
            if (result.IsFailure)
            {
                transaction.Rollback();
                return Result.Failure(EmployeeError.NotFound);
            }

            //Update the user row
            string userSqlQuery =
                @"UPDATE Persons.Users 
                SET FirstName=@FirstName, 
                LastName=@LastName, 
                Email=@Email, 
                PhoneNumber=@PhoneNumber, 
                Address=@Address, 
                City=@City, 
                ZipCode=@ZipCode, 
                Country=@Country, 
                BirthDate=@BirthDate, 
                AllowMarketing=@AllowMarketing, 
                UpdatedAt=@UpdatedAt 
                WHERE Persons.Users.Id = @Id";

            DynamicParameters userParameters = new DynamicParameters();
            userParameters.Add("@Id", result.Data.UserId);
            userParameters.Add("@FirstName", command.User.FirstName);
            userParameters.Add("@LastName", command.User.LastName);
            userParameters.Add("@Email", command.User.Email);
            userParameters.Add("@PhoneNumber", command.User.PhoneNumber);
            userParameters.Add("@Address", command.User.Address);
            userParameters.Add("@City", command.User.City);

            userParameters.Add("@ZipCode", command.User.ZipCode);
            userParameters.Add("@Country", command.User.Country);
            userParameters.Add("@BirthDate", command.User.BirthDate);
            userParameters.Add("@AllowMarketing", command.User.AllowMarketing);
            userParameters.Add("@UpdatedAt", DateTime.UtcNow);

            int affectedRows = 0;
            try
            {
                affectedRows = await _dbConnection.ExecuteAsync(
                    userSqlQuery,
                    userParameters,
                    transaction
                );
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex;
            }

            if (affectedRows is 0)
            {
                transaction.Rollback();
                return Result.Failure(EmployeeError.NotUpdated);
            }

            //Update the Employee row
            string employeeSqlQuery =
                @"UPDATE Persons.Employees 
                SET HiredAt=@HiredAt, ContractEndAt=@ContractEndAt, UpdatedAt=@UpdatedAt 
                WHERE Persons.Employees.Id=@Id";
            DynamicParameters employeeParameters = new DynamicParameters();
            employeeParameters.Add("@Id", command.Id);
            employeeParameters.Add("@HiredAt", command.Employee.HiredAt);
            employeeParameters.Add("@ContractEndAt", command.Employee.ContractEndAt);
            employeeParameters.Add("@UpdatedAt", DateTime.UtcNow);

            try
            {
                affectedRows = await _dbConnection.ExecuteAsync(
                    employeeSqlQuery,
                    employeeParameters,
                    transaction
                );
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex;
            }

            if (affectedRows is 0)
            {
                transaction.Rollback();
                return Result.Failure(EmployeeError.NotUpdated);
            }

            transaction.Commit();
            return Result.Success();
        }
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
