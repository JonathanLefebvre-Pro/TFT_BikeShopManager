using BikeShopManager.AppCore.Extensions;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;

namespace BikeShopManager.AppCore.Features.Commands.Employees;

public class EnableEmployeeCommandHandler(
    IEmployeeRepository EmployeeRepository,
    IAuthRepository AuthRepository
) : ICommandHandlerAsync<EnableEmployeeCommand>
{
    public async Task<Result> HandleAsync(EnableEmployeeCommand command)
    {
        bool isEmployeeExist = await EmployeeRepository.IsEmployeeExist(command.Id);
        if (!isEmployeeExist)
            return Result.Failure(EmployeeError.NotFound);

        bool result = await EmployeeRepository.EnableEmployee(command.Id);

        //TODO : Replace SecurityStamp and Revoke all tokens
        string securityStamp = SecurityStampExtension.GenerateSecurityStamp();
        await AuthRepository.RevokeAllActiveTokensAsync(command.Id);

        return result ? Result.Success() : Result.Failure(EmployeeError.NotEnabled);
    }
}
