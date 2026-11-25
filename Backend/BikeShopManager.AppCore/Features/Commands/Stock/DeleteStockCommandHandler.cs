using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public class DeleteStockCommandHandler<TEntity>(IStockRepository<TEntity> Repository)
    : ICommandHandlerAsync<DeleteStockCommand<TEntity>>
    where TEntity : StockEntityBase
{
    public async Task<Result> HandleAsync(DeleteStockCommand<TEntity> command)
    {
        return await Repository.DeleteAsync(command.Id)
            ? Result.Success()
            : Result.Failure(StockError<TEntity>.NotDeleted);
    }
}
