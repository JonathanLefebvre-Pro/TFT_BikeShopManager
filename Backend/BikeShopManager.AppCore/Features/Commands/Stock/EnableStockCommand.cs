using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities.Shared;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public record EnableStockCommand<TEntity>(int Id) : ICommandDefinition
    where TEntity : StockEntityBase, IActivable;
