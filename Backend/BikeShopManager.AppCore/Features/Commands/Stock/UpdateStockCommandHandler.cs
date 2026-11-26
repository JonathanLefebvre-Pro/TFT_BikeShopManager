using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public class UpdateStockCommandHandler<TEntity>(IStockRepository<TEntity> Repository)
    : ICommandHandlerAsync<UpdateStockCommand<TEntity>>
    where TEntity : StockEntityBase
{
    public async Task<Result> HandleAsync(UpdateStockCommand<TEntity> command)
    {
        try
        {
            return await Repository.UpdateAsync(command.Id, command.stockEntity)
                ? Result.Success()
                : Result.Failure(StockError<TEntity>.NotUpdated);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
