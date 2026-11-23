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
) : IQueryHandlerAsync<RefreshTokenQuery, AuthUser>
{
    public async Task<Result<AuthUser>> HandleAsync(RefreshTokenQuery query)
    {
        try
        {
            //Check if the refresh token exist on DB
            //  if the token does not exist : return unauthorized
            string refreshTokenHash = JwtProvider.HashRefreshToken(query.RefreshToken);
            RefreshToken? existingRefreshToken = await AuthRepository.GetRefreshTokenAsync(
                refreshTokenHash
            );
            if (existingRefreshToken is null)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            //If the token exist but is not active :
            //  check if the token is not revoked yet : if so revoked it and return unauthorized
            if (!existingRefreshToken.IsActive)
            {
                //If refresh token revoked date is not null -> Revoke all refresh tokens for that user
                if (existingRefreshToken.RevokedAt is not null)
                    await AuthRepository.RevokeAllActiveTokensAsync(
                        existingRefreshToken.EmployeeId
                    );

                return Result<AuthUser>.Failure(AuthError.NotAuthorized);
            }

            //Now that we are sur that the token exist and is still active
            //  Get the employee by employeeId
            Employee? employee = await EmployeeRepository.GetEmployeeById(
                existingRefreshToken.EmployeeId
            );
            //If the employee is null -> Reject
            if (employee is null)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            //If the employee account is disabled -> Reject
            if (!employee.IsActive)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            //Get user data by id
            User? user = await UserRepository.GetUserById(employee.UserId);
            if (user is null)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            //Set employer user data
            employee.User = user;

            //Get role data by employee id
            int roleId = await EmployeeRoleRepository.GetEmployeeRole(employee.Id);
            Role? role = await RoleRepository.GetRoleById(roleId);
            if (role is null)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            //Set employee role data
            employee.Role = role;

            //The user is verified -> generate a new token
            JwtAccessToken jwtAccessToken = await JwtProvider.GenerateAccessToken(employee);

            //Generate the refresh token
            JwtRefreshToken jwtRefreshToken = await JwtProvider.GenerateRefreshToken();

            //Create a new refreshToken via the jwtProvider service
            //  set the new expiration time
            RefreshToken refreshToken = new RefreshToken()
            {
                TokenHash = JwtProvider.HashRefreshToken(jwtRefreshToken.Token),
                EmployeeId = employee.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = jwtRefreshToken.ExpireAt,
            };

            //Replace the old token by the new one
            await AuthRepository.ReplaceTokenHash(
                existingRefreshToken.TokenHash,
                refreshToken.TokenHash
            );

            //Store the new refresh token object on DB
            int newRefreshTokenId = await AuthRepository.CreateRefreshToken(refreshToken);
            if (newRefreshTokenId is 0)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            AuthUser result = new AuthUser()
            {
                Id = employee.Id,
                Role = employee.Role,
                AccessToken = jwtAccessToken,
                RefreshToken = jwtRefreshToken,
            };

            return Result<AuthUser>.Success(result);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
