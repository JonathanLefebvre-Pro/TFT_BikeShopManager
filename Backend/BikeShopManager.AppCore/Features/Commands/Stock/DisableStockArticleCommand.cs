using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public record DisableStockArticleCommand(int Id) : ICommandDefinition;
