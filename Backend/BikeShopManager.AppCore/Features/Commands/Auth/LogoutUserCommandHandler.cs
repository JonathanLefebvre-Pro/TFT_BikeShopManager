using BikeShopManager.AppCore.Extensions;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.AppCore.Interfaces.Tokens;

namespace BikeShopManager.AppCore.Features.Commands.Auth;

public class LogoutUserCommandHandler(IAuthRepository AuthRepository, IJwtProvider JwtProvider)
    : ICommandHandlerAsync<LogoutUserCommand>
{
    public async Task<Result> HandleAsync(LogoutUserCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.RefreshToken))
            return Result.Failure(AuthError.NotValidToken);

        //TODO : Replace SecurityStamp
        string securityStamp = SecurityStampExtension.GenerateSecurityStamp();

        //TODO : Revoke all refresh tokens for this employee ?
        //AuthRepository.RevokeAllActiveTokensAsync(employee.Id);

        string refreshTokenHash = JwtProvider.HashRefreshToken(command.RefreshToken);
        bool result = await AuthRepository.RevokeRefreshTokenAsync(refreshTokenHash);
        return result ? Result.Success() : Result.Failure(AuthError.NotValidToken);
    }
}
