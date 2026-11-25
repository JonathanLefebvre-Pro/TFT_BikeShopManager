using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Queries.Stock;

public record GetStockQuery<TEntity> : IQueryDefinition<IEnumerable<TEntity>>
    where TEntity : StockEntityBase;
