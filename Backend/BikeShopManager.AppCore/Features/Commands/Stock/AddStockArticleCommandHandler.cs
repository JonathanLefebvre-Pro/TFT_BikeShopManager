using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public class AddStockArticleCommandHandler(IStockRepository<Article> Repository)
    : ICommandHandlerAsync<AddStockArticleCommand, int>
{
    public async Task<Result<int>> HandleAsync(AddStockArticleCommand command)
    {
        int newArticleId = 0;
        try
        {
            newArticleId = await Repository.AddAsync(command.article);
        }
        catch (Exception ex)
        {
            return ex;
        }

        return (newArticleId is 0)
            ? Result<int>.Failure(StockArticleError.NotInserted)
            : Result<int>.Success(newArticleId);
    }
}
