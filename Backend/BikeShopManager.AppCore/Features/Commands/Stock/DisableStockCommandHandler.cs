using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public class DisableStockCommandHandler<T>(IStockActivableRepository<T> Repository)
    : ICommandHandlerAsync<DisableStockCommand<T>>
    where T : StockBase
{
    public async Task<Result> HandleAsync(DisableStockCommand<T> command)
    {
        return await Repository.Disable(command.Id)
            ? Result.Success()
            : Result.Failure(StockError<T>.NotDisabled);
    }
}
