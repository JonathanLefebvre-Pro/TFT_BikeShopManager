using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;

namespace BikeShopManager.Infrastructure.Services;

public class UpdateEmployeeCommandHandler : ICommandHandlerAsync<UpdateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRoleRepository _employeeRoleRepository;

    public UpdateEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        IUserRepository userRepository,
        IEmployeeRoleRepository employeeRoleRepository
    )
    {
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
        _employeeRoleRepository = employeeRoleRepository;
    }

    public async Task<Result> HandleAsync(UpdateEmployeeCommand command)
    {
        //Get the employee userId from the employee id
        int userId = await _employeeRepository.GetEmployeeUserId(command.Id);

        if (userId is 0)
            return Result.Failure(EmployeeError.NotFound);

        //Update the user row
        try
        {
            bool isUserUpdated = await _userRepository.UpdateUser(userId, command.User);
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

        //Update the role id
        try
        {
            bool isRoleUpdated = await _employeeRoleRepository.UpdateEmployeeRole(
                command.Id,
                command.RoleId
            );
            if (!isRoleUpdated)
                return Result.Failure(RoleError.NotUpdated);
        }
        catch (Exception ex)
        {
            return ex;
        }

        return Result.Success();
    }
}
