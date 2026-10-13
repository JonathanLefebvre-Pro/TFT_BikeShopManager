using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands;

public record DisableCustomerCommand(int Id) : ICommandDefinition;
