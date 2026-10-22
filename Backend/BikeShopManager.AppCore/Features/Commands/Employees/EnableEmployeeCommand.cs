using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands;

public record EnableEmployeeCommand(int Id) : ICommandDefinition;
