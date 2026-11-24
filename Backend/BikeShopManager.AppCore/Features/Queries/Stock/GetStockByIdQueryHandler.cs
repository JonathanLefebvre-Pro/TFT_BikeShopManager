using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Queries.Stock;

public class GetStockByIdQueryHandler<T>(IStockRepository<T> Repository)
    : IQueryHandlerAsync<GetStockByIdQuery<T>, T>
    where T : StockBase
{
    public async Task<Result<T>> HandleAsync(GetStockByIdQuery<T> query)
    {
        T? obj = await Repository.GetAsync(query.Id);
        if (obj is null)
            return Result<T>.Failure(StockError<T>.NotFound);

        return obj;
    }
}
