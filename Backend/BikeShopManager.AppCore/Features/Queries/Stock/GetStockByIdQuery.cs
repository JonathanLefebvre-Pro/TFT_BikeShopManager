using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Queries.Stock;

public record GetStockByIdQuery<TEntity>(int Id) : IQueryDefinition<TEntity>
    where TEntity : StockEntityBase;
