using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands;

public record EnableCustomerCommand(int Id) : ICommandDefinition;
