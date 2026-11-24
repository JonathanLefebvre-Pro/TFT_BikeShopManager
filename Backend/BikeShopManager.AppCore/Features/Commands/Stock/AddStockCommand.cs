using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public record AddStockCommand<T>(T stockObj) : ICommandDefinition<int>
    where T : StockBase;
