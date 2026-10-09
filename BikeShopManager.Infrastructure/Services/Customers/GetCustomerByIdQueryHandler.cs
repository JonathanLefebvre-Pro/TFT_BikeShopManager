using System.Data;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Customers;

public class GetCustomerByIdQueryHandler : IQueryHandlerAsync<GetCustomerByIdQuery, Customer>
{
    private readonly IDbConnection _dbConnection;

    public GetCustomerByIdQueryHandler(IDbConnection DbConnection)
    {
        _dbConnection = DbConnection;
        _dbConnection.Open();
    }

    public async Task<Result<Customer>> HandleAsync(
        GetCustomerByIdQuery query,
        IDbTransaction transact = null!
    )
    {
        string customerSqlQuery =
            @"SELECT Id, UserId, VatNumber, IsActive, CreatedAt, UpdatedAt 
                FROM Persons.Customers 
                WHERE Persons.Customers.Id=@Id";

        DynamicParameters customerParameters = new DynamicParameters();
        customerParameters.Add("@Id", query.Id);

        Customer? customer = await _dbConnection.QuerySingleOrDefaultAsync<Customer>(
            customerSqlQuery,
            customerParameters,
            transact
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
            userParameters,
            transact
        );

        if (user is null)
            return Result<Customer>.Failure(CustomerError.NotFound);

        customer.User = user;
        return customer;
    }
}
