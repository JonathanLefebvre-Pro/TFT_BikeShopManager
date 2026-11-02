using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands.Employees;

public record EnableEmployeeCommand(int Id) : ICommandDefinition;
