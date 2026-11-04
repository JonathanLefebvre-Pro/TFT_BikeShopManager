using BikeShopManager.AppCore.Extensions;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.AppCore.Interfaces.Tokens;
using BikeShopManager.Domain.Entities;
using BikeShopManager.Domain.Entities.Auth;

namespace BikeShopManager.AppCore.Features.Queries.Auth;

public class LoginUserQueryHandler(
    IAuthRepository AuthRepository,
    IJwtProvider JwtProvider,
    IEmployeeRepository EmployeeRepository,
    IEmployeeRoleRepository EmployeeRoleRepository,
    IUserRepository UserRepository,
    IRoleRepository RoleRepository
) : IQueryHandlerAsync<LoginUserQuery, TokenResponse>
{
    public async Task<Result<TokenResponse>> HandleAsync(LoginUserQuery query)
    {
        try
        {
            //Try to authenticate user
            ExistingUser? existingUser = await UserRepository.GetUserByMail(query.Request.Login);
            if (existingUser is null)
                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);

            Employee? employee = await EmployeeRepository.GetEmployeeByUserId(existingUser.Id);
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

            //Verify password
            bool isPasswordVerified = HashingTools.VerifyPassword(
                query.Request.Password,
                employee.PasswordHash
            );

            if (!isPasswordVerified)
                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);

            //The user is authenticated -> generate a new AccessToken
            TokenResponse token = JwtProvider.GenerateToken(employee);

            //Create a new RefreshToken object from the one that is returned on the TokenResponse that also contains the AccessToken
            RefreshToken refreshToken = new RefreshToken()
            {
                TokenHash = JwtProvider.HashRefreshToken(token.RefreshToken),
                EmployeeId = employee.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = token.RefreshExpireAt,
            };

            //Store the new RefreshToken object on DB
            int newRefreshTokenId = await AuthRepository.CreateRefreshToken(refreshToken);
            if (newRefreshTokenId is 0)
                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);

            return Result<TokenResponse>.Success(token);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
