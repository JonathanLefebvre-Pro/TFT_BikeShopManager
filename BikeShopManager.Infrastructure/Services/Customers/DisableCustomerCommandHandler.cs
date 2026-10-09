using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using Dapper;

namespace BikeShopManager.Infrastructure.Services;

public class DisableCustomerCommandHandler : ICommandHandlerAsync<DisableCustomerCommand>
{
    private readonly IDbSession _dbSession;

    public DisableCustomerCommandHandler(IDbSession DbSession)
    {
        _dbSession = DbSession;
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

        int row = await _dbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
        if (row is 0)
            return Result.Failure(CustomerError.NotDisabled);

        return Result.Success();
    }
}
