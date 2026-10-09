using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Employees;

public class GetRolesQueryHandler : IQueryHandlerAsync<GetRolesQuery, IEnumerable<Role>>
{
    private readonly IDbSession _dbSession;

    public GetRolesQueryHandler(IDbSession DbSession)
    {
        _dbSession = DbSession;
    }

    public async Task<Result<IEnumerable<Role>>> HandleAsync(GetRolesQuery query)
    {
        string sqlQuery =
            @"SELECT Id, Name, NormalizedName, CreatedAt, UpdatedAt 
                FROM Persons.Roles";

        IEnumerable<Role> roles = await _dbSession.Connection.QueryAsync<Role>(
            sqlQuery,
            null,
            _dbSession.Transaction
        );

        if (roles is null || roles.Count() is 0)
            return RoleError.Empty;

        return Result<IEnumerable<Role>>.Success(roles);
    }
}
