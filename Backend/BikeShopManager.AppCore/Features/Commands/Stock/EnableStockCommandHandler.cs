using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public class EnableStockArticleCommandHandler<T>(IStockActivableRepository<T> Repository)
    : ICommandHandlerAsync<EnableStockCommand<T>>
    where T : StockBase
{
    public async Task<Result> HandleAsync(EnableStockCommand<T> command)
    {
        return await Repository.Enable(command.Id)
            ? Result.Success()
            : Result.Failure(StockError<T>.NotEnabled);
    }
}
