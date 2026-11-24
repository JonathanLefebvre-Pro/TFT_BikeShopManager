using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Queries.Stock;

public record GetStockByNameQuery<T>(string Name) : IQueryDefinition<T>
    where T : StockBase;
