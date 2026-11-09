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

        //Replace SecurityStamp
        string securityStamp = SecurityStampExtension.GenerateSecurityStamp();
        bool isSecurityStampUpdated = await EmployeeRepository.UpdateEmployeeSecurityStamp(
            command.Id,
            securityStamp
        );
        if (!isSecurityStampUpdated)
            Result.Failure(EmployeeError.NotEnabled);

        //Revoke all tokens
        await AuthRepository.RevokeAllActiveTokensAsync(command.Id);

        bool result = await EmployeeRepository.DisableEmployee(command.Id);

        return result ? Result.Success() : Result.Failure(EmployeeError.NotDisabled);
    }
}
