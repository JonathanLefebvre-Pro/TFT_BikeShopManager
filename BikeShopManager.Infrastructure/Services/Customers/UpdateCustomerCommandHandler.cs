using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services;

public class UpdateCustomerCommandHandler : ICommandHandlerAsync<UpdateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;

    public UpdateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUserRepository userRepository
    )
    {
        ;
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> HandleAsync(UpdateCustomerCommand command)
    {
        //Get the customer based on the command Id parameter
        //Get the user row linked to the customer UserId
        Customer? customer = await _customerRepository.GetCustomerById(command.Id);

        if (customer is null)
            return Result.Failure(CustomerError.NotFound);

        //Update the user row
        try
        {
            bool isUserUpdated = await _userRepository.UpdateUser(customer.UserId, command.User);
            if (!isUserUpdated)
                return Result.Failure(UserError.NotUpdated);
        }
        catch (Exception ex)
        {
            return ex;
        }

        //Update the customer row
        try
        {
            bool isCustomerUpdated = await _customerRepository.UpdateCustomer(
                command.Id,
                command.Customer.VatNumber
            );
            if (!isCustomerUpdated)
                return Result.Failure(CustomerError.NotUpdated);
        }
        catch (Exception ex)
        {
            return ex;
        }

        return Result.Success();
    }
}
