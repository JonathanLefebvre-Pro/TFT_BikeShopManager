using BikeShopManager.AppCore.Extensions;
using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services;

public class AddEmployeeCommandHandler : ICommandHandlerAsync<AddEmployeeCommand, int>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRoleRepository _employeeRoleRepository;

    public AddEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        IUserRepository userRepository,
        IEmployeeRoleRepository employeeRoleRepository
    )
    {
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
        _employeeRoleRepository = employeeRoleRepository;
    }

    public async Task<Result<int>> HandleAsync(AddEmployeeCommand command)
    {
        //Check if user already exist or not
        //If the user exist -> check values with the values inputed by the user
        //  if ok -> use the existing user
        //  if nok -> create a new user
        //If not -> create a new user
        ExistingUser? existingUser = await _userRepository.GetUserByMail(command.User.Email!);

        int userRowId = 0;
        bool isSameIdentity = false;
        if (existingUser is not null)
        {
            //Check if fields (email, FirstName, LastName and Birthdate) are the same between the existing user and the new one
            isSameIdentity = UserExtension.IsSameIdentity(existingUser, command.User);

            if (isSameIdentity)
                userRowId = existingUser.Id;
        }

        if (!isSameIdentity)
        {
            //Insert user data in User table and recover row id
            try
            {
                //Avoid crash if query failed (I.E : user with same email already exist !)
                userRowId = await _userRepository.CreateUser(command.User);
                if (userRowId is 0)
                    return Result<int>.Failure(UserError.NotInserted);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        //Insert Employee data in Employee table and recover row id
        int employeeRowId;
        try
        {
            employeeRowId = await _employeeRepository.CreateEmployee(userRowId, command.Employee);
            if (employeeRowId is 0)
                return Result<int>.Failure(EmployeeError.NotInserted);
        }
        catch (Exception ex)
        {
            return ex;
        }

        //Insert a row on the Persons.EmployeeRoles table
        bool result = await _employeeRoleRepository.CreateEmployeeRole(
            employeeRowId,
            command.RoleId
        );

        return result
            ? Result<int>.Success(employeeRowId)
            : Result<int>.Failure(EmployeeRoleError.NotInserted);
    }
}
