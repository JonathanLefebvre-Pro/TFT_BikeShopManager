using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public record UpdateStockCommand<TEntity>(TEntity stockEntity) : ICommandDefinition
    where TEntity : StockEntityBase;
