using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using BikeShopManager.Domain.Entities.Auth;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class AuthRepository(IDbSession DbSession) : IAuthRepository
{
    private const string _tableName = @"Persons.RefreshTokens";

    public async Task<Employee?> AuthenticateUserAsync(string login, string password)
    {
        //TODO : Check if the user exist, check password, login, ...

        //Get the user from the login (email ?)
        //Get the employee associated to this user
        //Check the password hash with the password hashed from the password in param

        return null;
    }

    /// <summary>
    /// Revoke all active refresh tokens from this employee
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    public async Task<bool> RevokeAllActiveTokensAsync(int employeeId)
    {
        string sqlQuery =
            @$"UPDATE ${_tableName} SET RevokedAt=@RevokedAt 
                WHERE EmployeeId=@EmployeeId 
                AND RevokedAt=NULL";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@RevokedAt", DateTime.UtcNow);
        parameters.Add("@EmployeeId", employeeId);

        int rows = await DbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );

        return rows is not 0;
    }

    /// <summary>
    /// Get this refresh token from DB
    /// </summary>
    /// <param name="refreshTokenHash"></param>
    /// <returns></returns>
    public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshTokenHash)
    {
        string sqlQuery = @$"SELECT TokenHash FROM ${_tableName} WHERE TokenHash=@TokenHash";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@TokenHash", refreshTokenHash);

        return await DbSession.Connection.QuerySingleOrDefaultAsync<RefreshToken>(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );
    }

    /// <summary>
    /// Add a new refresh token in DB
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    public async Task<int> CreateRefreshToken(RefreshToken refreshToken)
    {
        string sqlQuery =
            @$"INSERT INTO ${_tableName} (EmployeeId, TokenHash, ExpiresAt, CreatedAt) 
                OUTPUT INSERTED.ID 
                VALUES (@EmployeeId, @TokenHash, @ExpiresAt, @CreatedAt)";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", refreshToken.EmployeeId);
        parameters.Add("@TokenHash", refreshToken.TokenHash);
        parameters.Add("@ExpiresAt", refreshToken.ExpiresAt);
        parameters.Add("@CreatedAt", refreshToken.CreatedAt);

        return await DbSession.Connection.ExecuteScalarAsync<int>(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );
    }

    /// <summary>
    /// Revoke the selected refresh token
    /// </summary>
    /// <param name="refreshTokenHash"></param>
    /// <returns></returns>
    public async Task<bool> RevokeRefreshTokenAsync(string refreshTokenHash)
    {
        string sqlQuery =
            @$"UPDATE ${_tableName} SET RevokedAt=@RevokedAt WHERE TokenHash=@TokenHash";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@RevokedAt", DateTime.UtcNow);
        parameters.Add("@TokenHash", refreshTokenHash);

        int rows = await DbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );

        return rows is not 0;
    }

    public async Task<bool> UpdateReplacedByTokenHash(string oldTokenHash, string newTokenHash)
    {
        string sqlQuery =
            @$"UPDATE ${_tableName} SET ReplacedByTokenHash=@ReplacedByTokenHash, RevokedAt=@RevokedAt WHERE TokenHash=@TokenHash";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@ReplacedByTokenHash", newTokenHash);
        parameters.Add("@RevokedAt", DateTime.UtcNow);
        parameters.Add("@TokenHash", oldTokenHash);

        int rows = await DbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );

        return rows is not 0;
    }
}
