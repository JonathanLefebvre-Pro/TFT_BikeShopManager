using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands;

public record DeleteStockCommand<T>(int Id) : ICommandDefinition
    where T : StockBase;
