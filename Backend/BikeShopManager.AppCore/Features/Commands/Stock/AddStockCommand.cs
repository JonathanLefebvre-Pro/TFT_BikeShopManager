using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public record AddStockCommand<TEntity>(TEntity stockEntity) : ICommandDefinition<int>
    where TEntity : StockEntityBase;
