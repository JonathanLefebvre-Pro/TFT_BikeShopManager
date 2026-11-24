using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Queries.Stock;

public record GetStockQuery<T> : IQueryDefinition<IEnumerable<T>>
    where T : StockBase;
