using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;

namespace BikeShopManager.AppCore.Features.Commands.Customers;

public class EnableCustomerCommandHandler : ICommandHandlerAsync<EnableCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public EnableCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result> HandleAsync(EnableCustomerCommand command)
    {
        bool isCustomerExist = await _customerRepository.IsCustomerExist(command.Id);
        if (!isCustomerExist)
            return Result.Failure(CustomerError.NotFound);

        bool result = await _customerRepository.EnableCustomer(command.Id);

        return result ? Result.Success() : Result.Failure(CustomerError.NotEnabled);
    }
}
