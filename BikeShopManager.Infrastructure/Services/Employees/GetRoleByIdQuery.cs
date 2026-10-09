using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Employees;

public class GetRoleByIdQueryHandler : IQueryHandlerAsync<GetRoleByIdQuery, Role>
{
    private readonly IDbSession _dbSession;

    public GetRoleByIdQueryHandler(IDbSession DbSession)
    {
        _dbSession = DbSession;
    }

    public async Task<Result<Role>> HandleAsync(GetRoleByIdQuery query)
    {
        string sqlQuery =
            @"SELECT Id, Name, NormalizedName, CreatedAt, UpdatedAt 
                FROM Persons.Roles
                WHERE Id=@Id";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", query.Id);

        Role? role = await _dbSession.Connection.QuerySingleOrDefaultAsync<Role>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );

        if (role is null)
            return RoleError.NotFound;

        return role;
    }
}
