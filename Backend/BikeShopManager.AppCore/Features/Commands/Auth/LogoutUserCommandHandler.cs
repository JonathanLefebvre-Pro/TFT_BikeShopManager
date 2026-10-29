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

        string refreshTokenHash = JwtProvider.HashToken(command.RefreshToken);
        bool result = await AuthRepository.RevokeRefreshTokenAsync(refreshTokenHash);
        return result ? Result.Success() : Result.Failure(AuthError.NotValidToken);
    }
}
