using System.Data;
using BikeShopManager.AppCore.Extensions;
using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services;

public class AddEmployeeCommandHandler : ICommandHandlerAsync<AddEmployeeCommand, int>
{
    private readonly IDbSession _dbSession;

    public AddEmployeeCommandHandler(IDbSession DbSession)
    {
        _dbSession = DbSession;
    }

    public async Task<Result<int>> HandleAsync(AddEmployeeCommand command)
    {
        //Check if user already exist or not
        //If the user exist -> check values with the values inputed by the user
        //  if ok -> use the existing user
        //  if nok -> create a new user
        //If not -> create a new user
        string getUserSqlQuery =
            @"SELECT Id, FirstName, LastName, Email, BirthDate 
                    FROM Persons.Users
                    WHERE Email=@Email";
        DynamicParameters getUserParameters = new DynamicParameters();
        getUserParameters.Add("@Email", command.User.Email);
        ExistingUser? existingUser =
            await _dbSession.Connection.QuerySingleOrDefaultAsync<ExistingUser>(
                getUserSqlQuery,
                getUserParameters,
                _dbSession.Transaction
            );

        int userRowId = 0;
        bool isSameIdentity = false;
        if (existingUser is not null)
        {
            //Check if fields (email, FirstName, LastName and Birthdate) are the same between the existing user and the new one
            isSameIdentity = UserExtension.IsSameIdentity(existingUser, command.User);

            if (isSameIdentity)
                userRowId = existingUser.Id;
        }

        if (!isSameIdentity)
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

            try
            {
                //Avoid crash if query failed (I.E : user with same email already exist !)
                userRowId = await _dbSession.Connection.ExecuteScalarAsync<int>(
                    userSqlQuery,
                    userParameters,
                    _dbSession.Transaction
                );
            }
            catch (Exception ex)
            {
                return ex;
            }

            if (userRowId is 0)
            {
                return Result<int>.Failure(EmployeeError.NotInserted);
            }
        }

        //Insert Employee data in Employee table and recover row id
        string EmployeeSqlQuery =
            @"INSERT INTO Persons.Employees (UserId, PasswordHash, SecurityStamp, HiredAt, ContractEndAt) 
                    OUTPUT INSERTED.ID 
                    VALUES (@UserId, @PasswordHash, @SecurityStamp, @HiredAt, @ContractEndAt)";
        DynamicParameters employeeParameters = new DynamicParameters();
        employeeParameters.Add("@UserId", userRowId);
        employeeParameters.Add("@PasswordHash", command.Employee.PasswordHash);
        employeeParameters.Add("@SecurityStamp", command.Employee.SecurityStamp);
        employeeParameters.Add("@HiredAt", command.Employee.HiredAt);
        employeeParameters.Add("@ContractEndAt", command.Employee.ContractEndAt);

        int employeeRowId = 0;
        try
        {
            employeeRowId = await _dbSession.Connection.ExecuteScalarAsync<int>(
                EmployeeSqlQuery,
                employeeParameters,
                _dbSession.Transaction
            );
        }
        catch (Exception ex)
        {
            return ex;
        }

        if (employeeRowId is 0)
        {
            return Result<int>.Failure(EmployeeError.NotInserted);
        }

        return Result<int>.Success(employeeRowId);
    }
}
