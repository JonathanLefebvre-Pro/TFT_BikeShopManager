using System.Data;
using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using Dapper;

namespace BikeShopManager.Infrastructure.Services;

public class DisableCustomerCommandHandler : ICommandHandlerAsync<DisableCustomerCommand>
{
    private readonly IDbConnection _dbConnection;

    public DisableCustomerCommandHandler(IDbConnection DbConnection)
    {
        _dbConnection = DbConnection;
        _dbConnection.Open();
    }

    public async Task<Result> HandleAsync(
        DisableCustomerCommand command,
        IDbTransaction transact = null!
    )
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

        int row = await _dbConnection.ExecuteAsync(sqlQuery, parameters, transact);
        if (row is 0)
            return Result.Failure(CustomerError.NotDisabled);

        return Result.Success();
    }
}
