using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public class AddStockCommandHandler<T>(IStockRepository<T> Repository)
    : ICommandHandlerAsync<AddStockCommand<T>, int>
    where T : StockBase
{
    public async Task<Result<int>> HandleAsync(AddStockCommand<T> command)
    {
        int newArticleId = 0;
        try
        {
            newArticleId = await Repository.AddAsync(command.stockObj);
        }
        catch (Exception ex)
        {
            return ex;
        }

        return (newArticleId is 0)
            ? Result<int>.Failure(StockError<T>.NotInserted)
            : Result<int>.Success(newArticleId);
    }
}
