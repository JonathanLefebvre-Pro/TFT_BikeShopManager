using System.Data;
using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using Dapper;

namespace BikeShopManager.Infrastructure.Services;

public class EnableEmployeeCommandHandler : ICommandHandlerAsync<EnableEmployeeCommand>
{
    private readonly IDbSession _dbSession;

    public EnableEmployeeCommandHandler(IDbSession DbSession)
    {
        _dbSession = DbSession;
    }

    public async Task<Result> HandleAsync(EnableEmployeeCommand command)
    {
        string sqlQuery =
            @"UPDATE Persons.Employees 
                SET IsActive=@IsActive, UpdatedAt=@UpdatedAt
                WHERE Persons.Employees.Id = @Id 
                AND Persons.Employees.IsActive='false'";
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
            return Result.Failure(EmployeeError.NotEnabled);

        return Result.Success();
    }
}
