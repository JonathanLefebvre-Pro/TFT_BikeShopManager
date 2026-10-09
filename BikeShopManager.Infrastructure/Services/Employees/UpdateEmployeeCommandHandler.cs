using System.Data;
using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services;

public class UpdateEmployeeCommandHandler : ICommandHandlerAsync<UpdateEmployeeCommand>
{
    private readonly IDbSession _dbSession;
    private readonly IDispatcher _dispatcher;

    public UpdateEmployeeCommandHandler(IDbSession DbConnection, IDispatcher dispatcher)
    {
        _dbSession = DbConnection;
        _dispatcher = dispatcher;
    }

    public async Task<Result> HandleAsync(UpdateEmployeeCommand command)
    {
        //TODO : Check if connection is already opened
        //transact.Connection.State
        //Get the Employee based on the command Id parameter
        //Get the user row linked to the Employee UserId
        Result<Employee> result = await _dispatcher.SendQueryAsync<GetEmployeeByIdQuery, Employee>(
            new GetEmployeeByIdQuery(command.Id) /*,
                transaction*/
        );

        if (result.IsFailure)
        {
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
            affectedRows = await _dbSession.Connection.ExecuteAsync(
                userSqlQuery,
                userParameters,
                _dbSession.Transaction
            );
        }
        catch (Exception ex)
        {
            return ex;
        }

        if (affectedRows is 0)
        {
            return Result.Failure(EmployeeError.NotUpdated);
        }

        //Update the Employee row
        string employeeSqlQuery =
            @"UPDATE Persons.Employees 
                SET PasswordHash=@PasswordHash, SecurityStamp=@SecurityStamp, 
                HiredAt=@HiredAt, ContractEndAt=@ContractEndAt, UpdatedAt=@UpdatedAt
                WHERE Persons.Employees.Id=@Id";
        DynamicParameters employeeParameters = new DynamicParameters();
        employeeParameters.Add("@Id", command.Id);
        employeeParameters.Add("@PasswordHash", command.Employee.PasswordHash);
        employeeParameters.Add("@SecurityStamp", command.Employee.SecurityStamp);
        employeeParameters.Add("@HiredAt", command.Employee.HiredAt);
        employeeParameters.Add("@ContractEndAt", command.Employee.ContractEndAt);
        employeeParameters.Add("@UpdatedAt", DateTime.UtcNow);

        try
        {
            affectedRows = await _dbSession.Connection.ExecuteAsync(
                employeeSqlQuery,
                employeeParameters,
                _dbSession.Transaction
            );
        }
        catch (Exception ex)
        {
            return ex;
        }

        if (affectedRows is 0)
        {
            return Result.Failure(EmployeeError.NotUpdated);
        }

        return Result.Success();
    }
}
