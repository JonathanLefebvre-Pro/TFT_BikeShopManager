using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public record DeleteStockCommand<TEntity>(int Id) : ICommandDefinition
    where TEntity : StockEntityBase;
