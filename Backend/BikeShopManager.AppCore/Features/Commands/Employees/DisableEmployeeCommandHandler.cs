using BikeShopManager.AppCore.Extensions;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;

namespace BikeShopManager.AppCore.Features.Commands.Employees;

public class DisableEmployeeCommandHandler(
    IEmployeeRepository EmployeeRepository,
    IAuthRepository AuthRepository
) : ICommandHandlerAsync<DisableEmployeeCommand>
{
    public async Task<Result> HandleAsync(DisableEmployeeCommand command)
    {
        bool isEmployeeExist = await EmployeeRepository.IsEmployeeExist(command.Id);
        if (!isEmployeeExist)
            return Result.Failure(EmployeeError.NotFound);

        bool result = await EmployeeRepository.DisableEmployee(command.Id);

        //TODO : Replace SecurityStamp and Revoke all tokens
        string securityStamp = SecurityStampExtension.GenerateSecurityStamp();
        bool isRevoked = await AuthRepository.RevokeAllActiveTokensAsync(command.Id);
        if (!isRevoked)
            return Result.Failure(AuthError.NotValidToken);

        return result ? Result.Success() : Result.Failure(EmployeeError.NotDisabled);
    }
}
