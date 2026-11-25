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
        int newArticleId = 0;
        try
        {
            newArticleId = await Repository.AddAsync(command.stockEntity);
        }
        catch (Exception ex)
        {
            return ex;
        }

        return (newArticleId is 0)
            ? Result<int>.Failure(StockError<TEntity>.NotInserted)
            : Result<int>.Success(newArticleId);
    }
}
