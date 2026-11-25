using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Queries.Stock;

public class GetStockByNameQueryHandler<TEntity>(IStockRepository<TEntity> Repository)
    : IQueryHandlerAsync<GetStockByNameQuery<TEntity>, TEntity>
    where TEntity : StockEntityBase
{
    public async Task<Result<TEntity>> HandleAsync(GetStockByNameQuery<TEntity> query)
    {
        TEntity? obj = await Repository.GetAsync(query.Name);
        if (obj is null)
            return Result<TEntity>.Failure(StockError<TEntity>.NotFound);

        return obj;
    }
}
