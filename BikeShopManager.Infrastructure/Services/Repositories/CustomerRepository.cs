using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class CustomerRepository(IDbSession DbSession) : ICustomerRepository
{
    private const string _tableName = @"Persons.Customers";

    public async Task<int> CreateCustomer(int userId, string? vatNumber)
    {
        string insertCustomerSqlQuery =
            @$"INSERT INTO {_tableName} (UserId, VatNumber) 
                    OUTPUT INSERTED.ID 
                    VALUES (@UserId, @VatNumber)";
        DynamicParameters insertCustomerParameters = new DynamicParameters();
        insertCustomerParameters.Add("@UserId", userId);
        insertCustomerParameters.Add("@VatNumber", vatNumber);

        return await DbSession.Connection.ExecuteScalarAsync<int>(
            insertCustomerSqlQuery,
            insertCustomerParameters,
            DbSession.Transaction
        );
    }

    public async Task<bool> DisableCustomer(int id)
    {
        string sqlQuery =
            @$"UPDATE {_tableName} 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE {_tableName}.Id = @Id
                AND {_tableName}.IsActive='true'";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        parameters.Add("@IsActive", false);
        parameters.Add("@UpdatedAt", DateTime.UtcNow);

        int row = await DbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );

        return row is not 0;
    }

    public async Task<bool> EnableCustomer(int id)
    {
        string sqlQuery =
            @$"UPDATE {_tableName} 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE {_tableName}.Id = @Id 
                AND {_tableName}.IsActive='false'";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        parameters.Add("@IsActive", true);
        parameters.Add("@UpdatedAt", DateTime.UtcNow);

        int row = await DbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );

        return row is not 0;
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        string customerSqlQuery =
            @$"SELECT Id, UserId, VatNumber, IsActive, CreatedAt, UpdatedAt 
                FROM {_tableName} 
                WHERE {_tableName}.Id=@Id";

        DynamicParameters customerParameters = new DynamicParameters();
        customerParameters.Add("@Id", id);

        return await DbSession.Connection.QuerySingleOrDefaultAsync<Customer>(
            customerSqlQuery,
            customerParameters,
            DbSession.Transaction
        );
    }

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        string customerSqlQuery =
            @$"SELECT Id, UserId, VatNumber, IsActive, CreatedAt, UpdatedAt FROM {_tableName}";

        return await DbSession.Connection.QueryAsync<Customer>(
            customerSqlQuery,
            null,
            DbSession.Transaction
        );
    }

    public async Task<int> GetCustomerUserId(int id)
    {
        string customerSqlQuery = @$"SELECT UserId FROM {_tableName} WHERE {_tableName}.Id=@Id";

        DynamicParameters customerParameters = new DynamicParameters();
        customerParameters.Add("@Id", id);

        return await DbSession.Connection.QuerySingleOrDefaultAsync<int>(
            customerSqlQuery,
            customerParameters,
            DbSession.Transaction
        );
    }

    public async Task<bool> IsCustomerExist(int id)
    {
        string customerSqlQuery = @$"SELECT Id FROM {_tableName} WHERE {_tableName}.Id=@Id";

        DynamicParameters customerParameters = new DynamicParameters();
        customerParameters.Add("@Id", id);

        int customerId = await DbSession.Connection.QuerySingleOrDefaultAsync<int>(
            customerSqlQuery,
            customerParameters,
            DbSession.Transaction
        );

        return customerId is not 0;
    }

    public async Task<bool> UpdateCustomer(int id, string? vatNumber)
    {
        string customerSqlQuery =
            @$"UPDATE {_tableName} 
                SET VatNumber=@VatNumber, UpdatedAt=@UpdatedAt 
                WHERE {_tableName}.Id=@Id";
        DynamicParameters customerParameters = new DynamicParameters();
        customerParameters.Add("@Id", id);
        customerParameters.Add("@VatNumber", vatNumber);
        customerParameters.Add("@UpdatedAt", DateTime.UtcNow);

        int rows = await DbSession.Connection.ExecuteAsync(
            customerSqlQuery,
            customerParameters,
            DbSession.Transaction
        );

        return rows is not 0;
    }
}
