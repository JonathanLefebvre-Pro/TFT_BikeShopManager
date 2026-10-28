using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands.Employees;

public record DisableEmployeeCommand(int Id) : ICommandDefinition;
