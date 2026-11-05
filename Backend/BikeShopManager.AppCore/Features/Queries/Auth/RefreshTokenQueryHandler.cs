using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.AppCore.Interfaces.Tokens;
using BikeShopManager.Domain.Entities;
using BikeShopManager.Domain.Entities.Auth;

namespace BikeShopManager.AppCore.Features.Queries.Auth;

public class RefreshTokenQueryHandler(
    IAuthRepository AuthRepository,
    IJwtProvider JwtProvider,
    IEmployeeRepository EmployeeRepository,
    IEmployeeRoleRepository EmployeeRoleRepository,
    IUserRepository UserRepository,
    IRoleRepository RoleRepository
) : IQueryHandlerAsync<RefreshTokenQuery, TokenResponse>
{
    public async Task<Result<TokenResponse>> HandleAsync(RefreshTokenQuery query)
    {
        try
        {
            //Check if the refresh token exist on DB
            // if the token does not exist : return unauthorized
            string refreshTokenHash = JwtProvider.HashRefreshToken(query.RefreshToken);
            RefreshToken? existingRefreshToken = await AuthRepository.GetRefreshTokenAsync(
                refreshTokenHash
            );
            if (existingRefreshToken is null)
                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);

            // if the token exist but is not active :
            //  check if the token is not revoked yet : if so revoked it and return unauthorized
            if (!existingRefreshToken.IsActive)
            {
                if (existingRefreshToken.RevokedAt is not null)
                    await AuthRepository.RevokeAllActiveTokensAsync(
                        existingRefreshToken.EmployeeId
                    );

                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);
            }

            // Now that we are sur that the token exist and is still active
            //  Get the employee by employeeId
            //  if the employee is null : return unauthorized
            Employee? employee = await EmployeeRepository.GetEmployeeById(
                existingRefreshToken.EmployeeId
            );
            if (employee is null)
                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);

            if (!employee.IsActive)
                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);

            User? user = await UserRepository.GetUserById(employee.UserId);
            if (user is null)
                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);

            employee.User = user;

            int roleId = await EmployeeRoleRepository.GetEmployeeRole(employee.Id);
            Role? role = await RoleRepository.GetRoleById(roleId);
            if (role is null)
                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);

            employee.Role = role;

            //The user is verified -> generate a new token
            TokenResponse accessToken = JwtProvider.GenerateToken(employee);

            //Create a new refreshToken via the jwtProvider service
            //  set the new expiration time
            RefreshToken refreshToken = new RefreshToken()
            {
                TokenHash = JwtProvider.HashRefreshToken(accessToken.RefreshToken),
                EmployeeId = employee.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = accessToken.RefreshExpireAt,
            };

            //Set the revokedAt from the old refresh token to UtcNow
            //Set the replacedByTokenHash from the old refresh token by the new token hash
            await AuthRepository.UpdateReplacedByTokenHash(
                existingRefreshToken.TokenHash,
                refreshToken.TokenHash
            );

            //Store the new refresh token object on DB
            int newRefreshTokenId = await AuthRepository.CreateRefreshToken(refreshToken);
            if (newRefreshTokenId is 0)
                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);

            return Result<TokenResponse>.Success(accessToken);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
