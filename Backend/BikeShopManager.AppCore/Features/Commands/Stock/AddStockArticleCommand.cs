using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Features.Commands.Stock;

public record AddStockArticleCommand(Article article) : ICommandDefinition<int>;
