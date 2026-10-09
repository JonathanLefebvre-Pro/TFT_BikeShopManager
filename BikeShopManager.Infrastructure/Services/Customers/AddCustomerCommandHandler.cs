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

public class AddCustomerCommandHandler : ICommandHandlerAsync<AddCustomerCommand, int>
{
    private readonly IDbSession _dbSession;

    public AddCustomerCommandHandler(IDbSession DbSession)
    {
        _dbSession = DbSession;
    }

    public async Task<Result<int>> HandleAsync(AddCustomerCommand command)
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
            string insertUserSqlQuery =
                @"INSERT INTO Persons.Users (FirstName, LastName, Email, PhoneNumber, Address, City, ZipCode, Country, BirthDate, AllowMarketing) 
                    OUTPUT INSERTED.ID
                    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Address, @City, @ZipCode, @Country, @BirthDate, @AllowMarketing)";
            DynamicParameters insertUserParameters = new DynamicParameters();
            insertUserParameters.Add("@FirstName", command.User.FirstName);
            insertUserParameters.Add("@LastName", command.User.LastName);
            insertUserParameters.Add("@Email", command.User.Email);
            insertUserParameters.Add("@PhoneNumber", command.User.PhoneNumber);
            insertUserParameters.Add("@Address", command.User.Address);
            insertUserParameters.Add("@City", command.User.City);
            insertUserParameters.Add("@ZipCode", command.User.ZipCode);
            insertUserParameters.Add("@Country", command.User.Country);
            insertUserParameters.Add("@BirthDate", command.User.BirthDate);
            insertUserParameters.Add("@AllowMarketing", command.User.AllowMarketing);

            try
            {
                //Avoid crash if query failed (I.E : user with same email already exist !)
                userRowId = await _dbSession.Connection.ExecuteScalarAsync<int>(
                    insertUserSqlQuery,
                    insertUserParameters,
                    _dbSession.Transaction
                );
            }
            catch (Exception ex)
            {
                return ex;
            }

            if (userRowId is 0)
            {
                return Result<int>.Failure(CustomerError.NotInserted);
            }
        }

        //Insert Customer data in Customer table and recover row id
        string insertCustomerSqlQuery =
            @"INSERT INTO Persons.Customers (UserId, VatNumber) 
                    OUTPUT INSERTED.ID 
                    VALUES (@UserId, @VatNumber)";
        DynamicParameters insertCustomerParameters = new DynamicParameters();
        insertCustomerParameters.Add("@UserId", userRowId);
        insertCustomerParameters.Add("@VatNumber", command.Customer.VatNumber);

        int customerRowId = 0;
        try
        {
            customerRowId = await _dbSession.Connection.ExecuteScalarAsync<int>(
                insertCustomerSqlQuery,
                insertCustomerParameters,
                _dbSession.Transaction
            );
        }
        catch (Exception ex)
        {
            return ex;
        }

        if (customerRowId is 0)
        {
            return Result<int>.Failure(CustomerError.NotInserted);
        }

        return Result<int>.Success(customerRowId);
    }
}
