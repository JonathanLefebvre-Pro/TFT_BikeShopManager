using System.Data;
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
                return Result<int>.Failure(CustomerError.NotInserted);
            }

            //Insert Customer data in Customer table and recover row id
            string customerSqlQuery =
                @"INSERT INTO Persons.Customers (UserId, VatNumber) 
                    OUTPUT INSERTED.ID 
                    VALUES (@UserId, @VatNumber)";
            DynamicParameters customerParameters = new DynamicParameters();
            customerParameters.Add("@UserId", userRowId);
            customerParameters.Add("@VatNumber", command.Customer.VatNumber);

            int customerRowId = 0;
            try
            {
                customerRowId = await _dbConnection.ExecuteScalarAsync<int>(
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

            if (customerRowId is 0)
            {
                transaction.Rollback();
                return Result<int>.Failure(CustomerError.NotInserted);
            }

            transaction.Commit();
            return Result<int>.Success(customerRowId);
        }
    }

    public Task<Result> HandleAsync(DisableCustomerCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<Result> HandleAsync(UpdateCustomerCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Customer>> HandleAsync(GetCustomerByIdQuery query)
    {
        //SELECT * FROM Persons.Customers JOIN Persons.Users ON Persons.Customers.UserId = Persons.Users.Id
        string customerSqlQuery =
            @"SELECT Id, UserId, VatNumber, IsActive, CreatedAt, UpdatedAt 
                FROM Persons.Customers WHERE Persons.Customers.Id=@Id";

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
