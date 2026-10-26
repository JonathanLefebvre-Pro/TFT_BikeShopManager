using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using BikeShopManager.Domain.Entities.Auth;

namespace BikeShopManager.Infrastructure.Services.Auth;

public class LoginUserQueryHandler(IAuthRepository AuthRepository, IJwtProvider JwtProvider)
    : IQueryHandlerAsync<LoginUserQuery, TokenResponse>
{
    public async Task<Result<TokenResponse>> HandleAsync(LoginUserQuery query)
    {
        try
        {
            //Try to authenticate user
            Employee? employee = await AuthRepository.AuthenticateUserAsync(
                query.Request.Login,
                query.Request.Password
            );

            if (employee is null)
                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);

            //The user is authenticated -> generate a new token
            TokenResponse token = JwtProvider.GenerateToken(employee);

            //Create a new RefreshToken object
            RefreshToken refreshToken = new RefreshToken()
            {
                TokenHash = JwtProvider.HashToken(token.RefreshToken),
                EmployeeId = employee.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = token.RefreshExpireAt,
            };

            //Store the new refresh token object on DB
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
