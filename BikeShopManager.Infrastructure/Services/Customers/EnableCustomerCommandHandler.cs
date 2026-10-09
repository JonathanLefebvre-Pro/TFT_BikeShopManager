using System.Data;
using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using Dapper;

namespace BikeShopManager.Infrastructure.Services;

public class EnableCustomerCommandHandler : ICommandHandlerAsync<EnableCustomerCommand>
{
    private readonly IDbConnection _dbConnection;

    public EnableCustomerCommandHandler(IDbConnection DbConnection)
    {
        _dbConnection = DbConnection;
        _dbConnection.Open();
    }

    public async Task<Result> HandleAsync(
        EnableCustomerCommand command,
        IDbTransaction transact = null!
    )
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

        int row = await _dbConnection.ExecuteAsync(sqlQuery, parameters, transact);
        if (row is 0)
            return Result.Failure(CustomerError.NotEnabled);

        return Result.Success();
    }
}
