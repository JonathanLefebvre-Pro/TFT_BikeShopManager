using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;

namespace BikeShopManager.AppCore.Features.Commands.Auth;

public class LogoutUserCommandHandler(IAuthRepository AuthRepository)
    : ICommandHandlerAsync<LogoutUserCommand>
{
    public async Task<Result> HandleAsync(LogoutUserCommand command)
    {
        bool result = await AuthRepository.RevokeRefreshTokenAsync(command.RefreshToken);
        return result ? Result.Success() : Result.Failure(AuthError.NotValidToken);
    }
}
