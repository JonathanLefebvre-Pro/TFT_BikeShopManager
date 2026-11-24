using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public class DisableStockArticleCommandHandler(IStockActivableRepository<Article> Repository)
    : ICommandHandlerAsync<DisableStockArticleCommand>
{
    public async Task<Result> HandleAsync(DisableStockArticleCommand command)
    {
        return await Repository.Disable(command.Id)
            ? Result.Success()
            : Result.Failure(StockArticleError.NotDisabled);
    }
}
