using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public record EnableStockCommand<T>(int Id) : ICommandDefinition
    where T : StockBase;
