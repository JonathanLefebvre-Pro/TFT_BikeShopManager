using BikeShopManager.AppCore.Extensions;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands.Employees;

public class UpdateEmployeePasswordCommandHandler
    : ICommandHandlerAsync<UpdateEmployeePasswordCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeePasswordCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> HandleAsync(UpdateEmployeePasswordCommand command)
    {
        //Get the employee from the employee id
        Employee? employee = await _employeeRepository.GetEmployeeById(command.Id);

        if (employee is null)
            return Result.Failure(EmployeeError.NotFound);

        //Compare old password with user current password hash in database
        bool isOldPasswordValid = HashingTools.VerifyPassword(
            command.OldPassword,
            employee.PasswordHash
        );

        if (!isOldPasswordValid)
            return Result.Failure(EmployeeError.InvalidPassword);

        //Hash new password with Argon2
        string newPasswordHash = HashingTools.HashPassword(command.NewPassword);
        //Generate new SecurityStamp (For JWT Claims)
        Guid guid = Guid.NewGuid();
        string securityStamp = guid.ToString("N");

        //Update the Employee row
        try
        {
            bool isEmployeeUpdated = await _employeeRepository.UpdateEmployeePassword(
                command.Id,
                newPasswordHash,
                securityStamp
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
