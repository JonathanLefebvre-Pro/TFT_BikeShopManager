using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services.Auth;

public class LoginUserQueryHandler(IAuthRepository AuthRepository, IJwtRepository JwtRepository)
    : IQueryHandlerAsync<LoginUserQuery, TokenResponse>
{
    public async Task<Result<TokenResponse>> HandleAsync(LoginUserQuery query)
    {
        try
        {
            //TODO : Call the authentication service
            //Check if the user exist, check password, ...
            //If authentication succeed -> use the JwtTokenService to generate a new token
            // and send this token back to the client
            //else send back Unauthorized (Message : Invalid login or password !)
            //or BadRequest if the try catch failed

            Employee? employee = await AuthRepository.AuthenticateUser(
                query.Request.Login,
                query.Request.Password
            );

            if (employee is null)
                return Result<TokenResponse>.Failure(AuthError.NotAuthorized);

            TokenResponse token = await JwtRepository.GenerateToken();

            return Result<TokenResponse>.Success(token);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
