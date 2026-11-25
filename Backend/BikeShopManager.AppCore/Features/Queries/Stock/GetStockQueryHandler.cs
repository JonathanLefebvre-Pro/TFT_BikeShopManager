using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Queries.Stock;

public class GetStockQueryHandler<TEntity>(IStockRepository<TEntity> Repository)
    : IQueryHandlerAsync<GetStockQuery<TEntity>, IEnumerable<TEntity>>
    where TEntity : StockEntityBase
{
    public async Task<Result<IEnumerable<TEntity>>> HandleAsync(GetStockQuery<TEntity> query)
    {
        IEnumerable<TEntity> objs = await Repository.GetAsync();
        if (objs is null || objs.Count() is 0)
            return Result<IEnumerable<TEntity>>.Failure(StockError<TEntity>.NotFound);

        return Result<IEnumerable<TEntity>>.Success(objs);
    }
}
