using System.Data;
using BikeShopManager.AppCore.Extensions;
using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services;

public class CustomerService : ICustomerRepository
{
    private IDbConnection _dbConnection;

    public CustomerService(IDbConnection DbConnection)
    {
        _dbConnection = DbConnection;
        _dbConnection.Open();
    }

    public async Task<Result<int>> HandleAsync(AddCustomerCommand command)
    {
        using (IDbTransaction transaction = _dbConnection.BeginTransaction())
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
                await _dbConnection.QuerySingleOrDefaultAsync<ExistingUser>(
                    getUserSqlQuery,
                    getUserParameters,
                    transaction
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
                    userRowId = await _dbConnection.ExecuteScalarAsync<int>(
                        insertUserSqlQuery,
                        insertUserParameters,
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
                customerRowId = await _dbConnection.ExecuteScalarAsync<int>(
                    insertCustomerSqlQuery,
                    insertCustomerParameters,
                    transaction
                );
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex;
            }

            if (customerRowId is 0)
            {
                transaction.Rollback();
                return Result<int>.Failure(CustomerError.NotInserted);
            }

            transaction.Commit();
            return Result<int>.Success(customerRowId);
        }
    }

    public async Task<Result> HandleAsync(DisableCustomerCommand command)
    {
        string sqlQuery =
            @"UPDATE Persons.Customers 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE Persons.Customers.Id = @Id
                AND Persons.Customers.IsActive='true'";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", command.Id);
        parameters.Add("@IsActive", false);
        parameters.Add("@UpdatedAt", DateTime.UtcNow);

        int row = await _dbConnection.ExecuteAsync(sqlQuery, parameters);
        if (row is 0)
            return Result.Failure(CustomerError.NotDisabled);

        return Result.Success();
    }

    public async Task<Result> HandleAsync(EnableCustomerCommand command)
    {
        string sqlQuery =
            @"UPDATE Persons.Customers 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE Persons.Customers.Id = @Id 
                AND Persons.Customers.IsActive='false'";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", command.Id);
        parameters.Add("@IsActive", true);
        parameters.Add("@UpdatedAt", DateTime.UtcNow);

        int row = await _dbConnection.ExecuteAsync(sqlQuery, parameters);
        if (row is 0)
            return Result.Failure(CustomerError.NotEnabled);

        return Result.Success();
    }

    public async Task<Result> HandleAsync(UpdateCustomerCommand command)
    {
        using (IDbTransaction transaction = _dbConnection.BeginTransaction())
        {
            //Get the customer based on the command Id parameter
            //Get the user row linked to the customer UserId
            Result<Customer> result = await HandleAsync(new GetCustomerByIdQuery(command.Id));
            if (result.IsFailure)
            {
                transaction.Rollback();
                return Result.Failure(CustomerError.NotFound);
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
                return Result.Failure(CustomerError.NotUpdated);
            }

            //Update the customer row
            string customerSqlQuery =
                @"UPDATE Persons.Customers 
                SET VatNumber=@VatNumber, UpdatedAt=@UpdatedAt 
                WHERE Persons.Customers.Id=@Id";
            DynamicParameters customerParameters = new DynamicParameters();
            customerParameters.Add("@Id", command.Id);
            customerParameters.Add("@VatNumber", command.Customer.VatNumber);
            customerParameters.Add("@UpdatedAt", DateTime.UtcNow);

            try
            {
                affectedRows = await _dbConnection.ExecuteAsync(
                    customerSqlQuery,
                    customerParameters,
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
                return Result.Failure(CustomerError.NotUpdated);
            }

            transaction.Commit();
            return Result.Success();
        }
    }

    public async Task<Result<Customer>> HandleAsync(GetCustomerByIdQuery query)
    {
        string customerSqlQuery =
            @"SELECT Id, UserId, VatNumber, IsActive, CreatedAt, UpdatedAt 
                FROM Persons.Customers 
                WHERE Persons.Customers.Id=@Id";

        DynamicParameters customerParameters = new DynamicParameters();
        customerParameters.Add("@Id", query.Id);

        Customer? customer = await _dbConnection.QuerySingleOrDefaultAsync<Customer>(
            customerSqlQuery,
            customerParameters
        );

        if (customer is null)
            return Result<Customer>.Failure(CustomerError.NotFound);

        string userSqlQuery =
            @"SELECT Id, FirstName, LastName , Email , PhoneNumber , Address , City , ZipCode , Country , BirthDate , AllowMarketing, CreatedAt, UpdatedAt
                FROM Persons.Users WHERE Persons.Users.Id = @UserId";
        DynamicParameters userParameters = new DynamicParameters();
        userParameters.Add("@UserId", customer.UserId);

        User? user = await _dbConnection.QuerySingleOrDefaultAsync<User>(
            userSqlQuery,
            userParameters
        );

        if (user is null)
            return Result<Customer>.Failure(CustomerError.NotFound);

        customer.User = user;
        return customer;
    }
}
