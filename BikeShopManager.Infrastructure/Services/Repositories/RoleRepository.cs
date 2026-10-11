using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IDbSession _dbSession;

    public RoleRepository(IDbSession dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<Role?> GetRoleById(int id)
    {
        string sqlQuery =
            @"SELECT Id, Name, NormalizedName, CreatedAt, UpdatedAt 
                FROM Persons.Roles
                WHERE Id=@Id";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Role>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public async Task<IEnumerable<Role>> GetRoles()
    {
        string sqlQuery =
            @"SELECT Id, Name, NormalizedName, CreatedAt, UpdatedAt 
                FROM Persons.Roles";

        return await _dbSession.Connection.QueryAsync<Role>(sqlQuery, null, _dbSession.Transaction);
    }
}
