using System.Data;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Customers;

public class GetCustomerByIdQueryHandler : IQueryHandlerAsync<GetCustomerByIdQuery, Customer>
{
    private readonly IDbSession _dbSession;

    public GetCustomerByIdQueryHandler(IDbSession DbSession)
    {
        _dbSession = DbSession;
    }

    public async Task<Result<Customer>> HandleAsync(GetCustomerByIdQuery query)
    {
        string customerSqlQuery =
            @"SELECT Id, UserId, VatNumber, IsActive, CreatedAt, UpdatedAt 
                FROM Persons.Customers 
                WHERE Persons.Customers.Id=@Id";

        DynamicParameters customerParameters = new DynamicParameters();
        customerParameters.Add("@Id", query.Id);

        Customer? customer = await _dbSession.Connection.QuerySingleOrDefaultAsync<Customer>(
            customerSqlQuery,
            customerParameters,
            _dbSession.Transaction
        );

        if (customer is null)
            return Result<Customer>.Failure(CustomerError.NotFound);

        string userSqlQuery =
            @"SELECT Id, FirstName, LastName , Email , PhoneNumber , Address , City , ZipCode , Country , BirthDate , AllowMarketing, CreatedAt, UpdatedAt
                FROM Persons.Users WHERE Persons.Users.Id = @UserId";
        DynamicParameters userParameters = new DynamicParameters();
        userParameters.Add("@UserId", customer.UserId);

        User? user = await _dbSession.Connection.QuerySingleOrDefaultAsync<User>(
            userSqlQuery,
            userParameters,
            _dbSession.Transaction
        );

        if (user is null)
            return Result<Customer>.Failure(CustomerError.NotFound);

        customer.User = user;
        return customer;
    }
}
