using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbSession _dbSession;

    public CustomerRepository(IDbSession dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<int> CreateCustomer(int userId, string? vatNumber)
    {
        string insertCustomerSqlQuery =
            @"INSERT INTO Persons.Customers (UserId, VatNumber) 
                    OUTPUT INSERTED.ID 
                    VALUES (@UserId, @VatNumber)";
        DynamicParameters insertCustomerParameters = new DynamicParameters();
        insertCustomerParameters.Add("@UserId", userId);
        insertCustomerParameters.Add("@VatNumber", vatNumber);

        return await _dbSession.Connection.ExecuteScalarAsync<int>(
            insertCustomerSqlQuery,
            insertCustomerParameters,
            _dbSession.Transaction
        );
    }

    public async Task<bool> DisableCustomer(int id)
    {
        string sqlQuery =
            @"UPDATE Persons.Customers 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE Persons.Customers.Id = @Id
                AND Persons.Customers.IsActive='true'";
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

    public async Task<bool> EnableCustomer(int id)
    {
        string sqlQuery =
            @"UPDATE Persons.Customers 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE Persons.Customers.Id = @Id 
                AND Persons.Customers.IsActive='false'";
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

    public async Task<Customer?> GetCustomerById(int id)
    {
        string customerSqlQuery =
            @"SELECT Id, UserId, VatNumber, IsActive, CreatedAt, UpdatedAt 
                FROM Persons.Customers 
                WHERE Persons.Customers.Id=@Id";

        DynamicParameters customerParameters = new DynamicParameters();
        customerParameters.Add("@Id", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Customer>(
            customerSqlQuery,
            customerParameters,
            _dbSession.Transaction
        );
    }

    public async Task<bool> UpdateCustomer(int id, string? vatNumber)
    {
        string customerSqlQuery =
            @"UPDATE Persons.Customers 
                SET VatNumber=@VatNumber, UpdatedAt=@UpdatedAt 
                WHERE Persons.Customers.Id=@Id";
        DynamicParameters customerParameters = new DynamicParameters();
        customerParameters.Add("@Id", id);
        customerParameters.Add("@VatNumber", vatNumber);
        customerParameters.Add("@UpdatedAt", DateTime.UtcNow);

        int rows = await _dbSession.Connection.ExecuteAsync(
            customerSqlQuery,
            customerParameters,
            _dbSession.Transaction
        );

        return rows is not 0;
    }
}
