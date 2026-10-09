using System.Data;
using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Employees;

public class DisableEmployeeCommandHandler : ICommandHandlerAsync<DisableEmployeeCommand>
{
    private readonly IDbConnection _dbConnection;

    public DisableEmployeeCommandHandler(IDbConnection DbConnection)
    {
        _dbConnection = DbConnection;
        _dbConnection.Open();
    }

    public async Task<Result> HandleAsync(
        DisableEmployeeCommand command,
        IDbTransaction transact = null!
    )
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

        int row = await _dbConnection.ExecuteAsync(sqlQuery, parameters, transact);
        if (row is 0)
            return Result.Failure(EmployeeError.NotDisabled);

        return Result.Success();
    }
}
