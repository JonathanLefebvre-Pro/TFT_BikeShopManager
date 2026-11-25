using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Shared;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public class EnableStockArticleCommandHandler<TEntity>(
    IStockActivableRepository<TEntity> Repository
) : ICommandHandlerAsync<EnableStockCommand<TEntity>>
    where TEntity : StockEntityBase, IActivable
{
    public async Task<Result> HandleAsync(EnableStockCommand<TEntity> command)
    {
        return await Repository.Enable(command.Id)
            ? Result.Success()
            : Result.Failure(StockError<TEntity>.NotEnabled);
    }
}
