using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;

namespace BikeShopManager.AppCore.Features.Commands.Employees;

public class DisableEmployeeCommandHandler(IEmployeeRepository EmployeeRepository)
    : ICommandHandlerAsync<DisableEmployeeCommand>
{
    public async Task<Result> HandleAsync(DisableEmployeeCommand command)
    {
        bool isEmployeeExist = await EmployeeRepository.IsEmployeeExist(command.Id);
        if (!isEmployeeExist)
            return Result.Failure(EmployeeError.NotFound);

        bool result = await EmployeeRepository.DisableEmployee(command.Id);
        return result ? Result.Success() : Result.Failure(EmployeeError.NotDisabled);
    }
}
