using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;

namespace BikeShopManager.Infrastructure.Services.Employees;

public class DisableEmployeeCommandHandler : ICommandHandlerAsync<DisableEmployeeCommand>
{
    private IEmployeeRepository _employeeRepository;

    public DisableEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> HandleAsync(DisableEmployeeCommand command)
    {
        bool result = await _employeeRepository.DisableEmployee(command.Id);
        return result ? Result.Success() : Result.Failure(EmployeeError.NotDisabled);
    }
}
