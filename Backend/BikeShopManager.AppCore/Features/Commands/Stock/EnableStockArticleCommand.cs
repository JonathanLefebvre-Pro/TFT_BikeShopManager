using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public record EnableStockArticleCommand(int Id) : ICommandDefinition;
