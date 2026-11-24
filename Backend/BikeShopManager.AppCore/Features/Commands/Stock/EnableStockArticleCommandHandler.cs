using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public class EnableStockArticleCommandHandler(IStockActivableRepository<Article> Repository)
    : ICommandHandlerAsync<EnableStockArticleCommand>
{
    public async Task<Result> HandleAsync(EnableStockArticleCommand command)
    {
        return await Repository.Enable(command.Id)
            ? Result.Success()
            : Result.Failure(StockArticleError.NotEnabled);
    }
}
