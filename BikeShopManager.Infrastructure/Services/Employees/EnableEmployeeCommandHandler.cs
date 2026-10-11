using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;

namespace BikeShopManager.Infrastructure.Services;

public class EnableEmployeeCommandHandler : ICommandHandlerAsync<EnableEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public EnableEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> HandleAsync(EnableEmployeeCommand command)
    {
        bool result = await _employeeRepository.EnableEmployee(command.Id);

        return result ? Result.Success() : Result.Failure(EmployeeError.NotEnabled);
    }
}
