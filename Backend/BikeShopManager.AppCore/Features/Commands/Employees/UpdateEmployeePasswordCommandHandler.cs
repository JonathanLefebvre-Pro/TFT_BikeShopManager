using BikeShopManager.AppCore.Extensions;
using BikeShopManager.AppCore.Extensions.Argon2;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands.Employees;

public class UpdateEmployeePasswordCommandHandler(
    IEmployeeRepository EmployeeRepository,
    IAuthRepository AuthRepository
) : ICommandHandlerAsync<UpdateEmployeePasswordCommand>
{
    public async Task<Result> HandleAsync(UpdateEmployeePasswordCommand command)
    {
        //Get the employee from the employee id
        Employee? employee = await EmployeeRepository.GetEmployeeById(command.Id);

        if (employee is null)
            return Result.Failure(EmployeeError.NotFound);

        //Compare old password with user current password hash in database
        bool isOldPasswordValid = PasswordHashExtension.VerifyPassword(
            command.OldPassword,
            employee.PasswordHash
        );

        if (!isOldPasswordValid)
            return Result.Failure(EmployeeError.InvalidPassword);

        //Hash new password with Argon2
        string newPasswordHash = PasswordHashExtension.HashPassword(command.NewPassword);

        //Generate new SecurityStamp (For JWT Claims)
        string securityStamp = SecurityStampExtension.GenerateSecurityStamp();

        //Update the Employee row
        try
        {
            bool isEmployeeUpdated = await EmployeeRepository.UpdateEmployeePassword(
                command.Id,
                newPasswordHash,
                securityStamp
            );
            if (!isEmployeeUpdated)
                return Result.Failure(EmployeeError.NotUpdated);

            //Revoke all refresh tokens for this employee
            bool revoked = await AuthRepository.RevokeAllActiveTokensAsync(employee.Id);
            if (!revoked)
                return Result.Failure(AuthError.NotValidToken);
        }
        catch (Exception ex)
        {
            return ex;
        }

        return Result.Success();
    }
}
