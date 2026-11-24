using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Queries.Stock;

public class GetStockByNameQueryHandler<T>(IStockRepository<T> Repository)
    : IQueryHandlerAsync<GetStockByNameQuery<T>, T>
    where T : StockBase
{
    public async Task<Result<T>> HandleAsync(GetStockByNameQuery<T> query)
    {
        T? obj = await Repository.GetAsync(query.Name);
        if (obj is null)
            return Result<T>.Failure(StockError<T>.NotFound);

        return obj;
    }
}
