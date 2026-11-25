using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Shared;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public class DisableStockCommandHandler<TEntity>(IStockActivableRepository<TEntity> Repository)
    : ICommandHandlerAsync<DisableStockCommand<TEntity>>
    where TEntity : StockEntityBase, IActivable
{
    public async Task<Result> HandleAsync(DisableStockCommand<TEntity> command)
    {
        return await Repository.Disable(command.Id)
            ? Result.Success()
            : Result.Failure(StockError<TEntity>.NotDisabled);
    }
}
