using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands;

public record DisableEmployeeCommand(int Id) : ICommandDefinition;
