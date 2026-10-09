using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using Dapper;

namespace BikeShopManager.Infrastructure.Services;

public class EnableCustomerCommandHandler : ICommandHandlerAsync<EnableCustomerCommand>
{
    private readonly IDbSession _dbSession;

    public EnableCustomerCommandHandler(IDbSession DbSession)
    {
        _dbSession = DbSession;
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

        int row = await _dbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
        if (row is 0)
            return Result.Failure(CustomerError.NotEnabled);

        return Result.Success();
    }
}
