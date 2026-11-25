using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Queries.Stock;

public record GetStockByNameQuery<TEntity>(string Name) : IQueryDefinition<TEntity>
    where TEntity : StockEntityBase;
