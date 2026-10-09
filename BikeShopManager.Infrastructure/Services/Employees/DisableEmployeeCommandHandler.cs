using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Employees;

public class DisableEmployeeCommandHandler : ICommandHandlerAsync<DisableEmployeeCommand>
{
    private readonly IDbSession _dbSession;

    public DisableEmployeeCommandHandler(IDbSession DbSession)
    {
        _dbSession = DbSession;
    }

    public async Task<Result> HandleAsync(DisableEmployeeCommand command)
    {
        string sqlQuery =
            @"UPDATE Persons.Employees 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE Persons.Employees.Id = @Id
                AND Persons.Employees.IsActive='true'";
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
            return Result.Failure(EmployeeError.NotDisabled);

        return Result.Success();
    }
}
