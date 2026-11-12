using BikeShopManager.AppCore.Extensions.Argon2;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.AppCore.Interfaces.Tokens;
using BikeShopManager.Domain.Entities;
using BikeShopManager.Domain.Entities.Auth;
using Microsoft.Extensions.Logging;

namespace BikeShopManager.AppCore.Features.Queries.Auth;

public class LoginQueryHandler(
    IAuthRepository AuthRepository,
    IJwtProvider JwtProvider,
    IEmployeeRepository EmployeeRepository,
    IEmployeeRoleRepository EmployeeRoleRepository,
    IUserRepository UserRepository,
    IRoleRepository RoleRepository,
    ILogger<LoginQueryHandler> Logger
) : IQueryHandlerAsync<LoginQuery, AuthUser>
{
    public async Task<Result<AuthUser>> HandleAsync(LoginQuery query)
    {
        Logger.LogInformation("Entering login query");
        try
        {
            //Try to authenticate user
            //Check if the user exist by email (login)
            ExistingUser? existingUser = await UserRepository.GetUserByMail(query.Login);
            if (existingUser is null)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            //Get employee data by user id
            Employee? employee = await EmployeeRepository.GetEmployeeByUserId(existingUser.Id);
            if (employee is null)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            //If employee account is disabled -> reject
            if (!employee.IsActive)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            //Get user data by id
            User? user = await UserRepository.GetUserById(employee.UserId);
            if (user is null)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            //Set employer user data
            employee.User = user;

            //Get employee role
            int roleId = await EmployeeRoleRepository.GetEmployeeRole(employee.Id);
            Role? role = await RoleRepository.GetRoleById(roleId);
            if (role is null)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            //Set employee role data
            employee.Role = role;

            //Verify password (check given password with stored password hash in DB)
            bool isPasswordVerified = PasswordHashExtension.VerifyPassword(
                query.Password,
                employee.PasswordHash
            );

            //If password is not valid -> Reject
            if (!isPasswordVerified)
                return Result<AuthUser>.Failure(AuthError.NotAuthorized);

            //The user is authenticated -> generate a new AccessToken
            JwtAccessToken jwtAccessToken = JwtProvider.GenerateAccessToken(employee);

            //Generate the refresh token
            JwtRefreshToken jwtRefreshToken = JwtProvider.GenerateRefreshToken();

            //Create a new RefreshToken object from the one that is returned on the TokenResponse that also contains the AccessToken
            RefreshToken refreshToken = new RefreshToken()
            {
                TokenHash = JwtProvider.HashRefreshToken(jwtRefreshToken.Token),
                EmployeeId = employee.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = jwtAccessToken.ExpireAt,
            };

            //Store the new RefreshToken object on DB
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

            //Return the token response object (contains Access/Refresh tokens)
            Logger.LogInformation("Exiting login query");
            return Result<AuthUser>.Success(result);
        }
        catch (Exception ex)
        {
            Logger.LogError("Error : {Message}", ex.Message);
            return ex;
        }
    }
}
