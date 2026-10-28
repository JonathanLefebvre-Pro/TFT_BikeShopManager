using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands.Customers;

public record EnableCustomerCommand(int Id) : ICommandDefinition;
