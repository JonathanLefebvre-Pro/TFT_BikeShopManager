using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public class AddStockCommandHandler<TEntity>(IStockRepository<TEntity> Repository)
    : ICommandHandlerAsync<AddStockCommand<TEntity>, int>
    where TEntity : StockEntityBase
{
    public async Task<Result<int>> HandleAsync(AddStockCommand<TEntity> command)
    {
        try
        {
            int newArticleId = await Repository.AddAsync(command.stockEntity);
            return (newArticleId is not 0)
                ? Result<int>.Success(newArticleId)
                : Result<int>.Failure(StockError<TEntity>.NotInserted);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
