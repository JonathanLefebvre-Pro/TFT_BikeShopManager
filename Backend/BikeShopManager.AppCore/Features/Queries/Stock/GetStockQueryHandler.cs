using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Queries.Stock;

public class GetStockQueryHandler<T>(IStockRepository<T> Repository)
    : IQueryHandlerAsync<GetStockQuery<T>, IEnumerable<T>>
    where T : StockBase
{
    public async Task<Result<IEnumerable<T>>> HandleAsync(GetStockQuery<T> query)
    {
        IEnumerable<T> objs = await Repository.GetAsync();
        if (objs is null || objs.Count() is 0)
            return Result<IEnumerable<T>>.Failure(StockError<T>.NotFound);

        return Result<IEnumerable<T>>.Success(objs);
    }
}
