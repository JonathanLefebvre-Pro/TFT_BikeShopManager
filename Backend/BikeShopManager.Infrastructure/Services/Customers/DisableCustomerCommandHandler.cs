using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;

namespace BikeShopManager.Infrastructure.Services;

public class DisableCustomerCommandHandler : ICommandHandlerAsync<DisableCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public DisableCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result> HandleAsync(DisableCustomerCommand command)
    {
        bool isCustomerExist = await _customerRepository.IsCustomerExist(command.Id);
        if (!isCustomerExist)
            return Result.Failure(CustomerError.NotFound);

        bool result = await _customerRepository.DisableCustomer(command.Id);

        return result ? Result.Success() : Result.Failure(CustomerError.NotDisabled);
    }
}
