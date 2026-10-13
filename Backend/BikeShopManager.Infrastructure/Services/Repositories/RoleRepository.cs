using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class RoleRepository(IDbSession DbSession) : IRoleRepository
{
    private const string _tableName = @"Persons.Roles";

    public async Task<Role?> GetRoleById(int id)
    {
        string sqlQuery =
            @$"SELECT Id, Name, NormalizedName, CreatedAt, UpdatedAt 
                FROM {_tableName}
                WHERE Id=@Id";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await DbSession.Connection.QuerySingleOrDefaultAsync<Role>(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );
    }

    public async Task<IEnumerable<Role>> GetRoles()
    {
        string sqlQuery =
            @$"SELECT Id, Name, NormalizedName, CreatedAt, UpdatedAt 
                FROM {_tableName}";

        return await DbSession.Connection.QueryAsync<Role>(sqlQuery, null, DbSession.Transaction);
    }

    public async Task<bool> IsRoleExist(int id)
    {
        string sqlQuery = @$"SELECT Id FROM {_tableName} WHERE Id=@Id";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        int roleId = await DbSession.Connection.QuerySingleOrDefaultAsync<int>(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );

        return roleId is not 0;
    }
}
