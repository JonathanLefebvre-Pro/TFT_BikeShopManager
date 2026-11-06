using BikeShopManager.AppCore.Extensions.Users;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands.Customers;

public class AddCustomerCommandHandler : ICommandHandlerAsync<AddCustomerCommand, int>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;

    public AddCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUserRepository userRepository
    )
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<int>> HandleAsync(AddCustomerCommand command)
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
            isSameIdentity = UsersExtension.IsSameIdentity(existingUser, command.User);

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

        //Insert Customer data in Customer table and recover row id
        int customerRowId;
        try
        {
            customerRowId = await _customerRepository.CreateCustomer(
                userRowId,
                command.Customer.VatNumber
            );
            if (customerRowId is 0)
                return Result<int>.Failure(CustomerError.NotInserted);
        }
        catch (Exception ex)
        {
            return ex;
        }

        return Result<int>.Success(customerRowId);
    }
}
