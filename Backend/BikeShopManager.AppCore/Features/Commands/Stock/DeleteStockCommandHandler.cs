using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands;

public class DeleteStockCommandHandler<T>(IStockRepository<T> Repository)
    : ICommandHandlerAsync<DeleteStockCommand<T>>
    where T : StockBase
{
    public async Task<Result> HandleAsync(DeleteStockCommand<T> command)
    {
        return await Repository.DeleteAsync(command.Id)
            ? Result.Success()
            : Result.Failure(StockError<T>.NotDeleted);
    }
}
