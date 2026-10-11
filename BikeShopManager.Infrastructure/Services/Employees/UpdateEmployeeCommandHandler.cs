using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services;

public class UpdateEmployeeCommandHandler : ICommandHandlerAsync<UpdateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserRepository _userRepository;

    public UpdateEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        IUserRepository userRepository
    )
    {
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> HandleAsync(UpdateEmployeeCommand command)
    {
        //Get the Employee based on the command Id parameter
        //Get the user row linked to the Employee UserId
        Employee? employee = await _employeeRepository.GetEmployeeById(command.Id);

        if (employee is null)
            return Result.Failure(EmployeeError.NotFound);

        //Update the user row
        try
        {
            bool isUserUpdated = await _userRepository.UpdateUser(employee.UserId, command.User);
            if (!isUserUpdated)
                return Result.Failure(UserError.NotUpdated);
        }
        catch (Exception ex)
        {
            return ex;
        }

        //Update the Employee row
        try
        {
            bool isEmployeeUpdated = await _employeeRepository.UpdateEmployee(
                command.Id,
                command.Employee
            );
            if (!isEmployeeUpdated)
                return Result.Failure(EmployeeError.NotUpdated);
        }
        catch (Exception ex)
        {
            return ex;
        }

        return Result.Success();
    }
}
