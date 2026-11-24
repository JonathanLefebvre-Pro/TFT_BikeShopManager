using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Queries.Stock;

public record GetStockByIdQuery<T>(int Id) : IQueryDefinition<T>
    where T : StockBase;
