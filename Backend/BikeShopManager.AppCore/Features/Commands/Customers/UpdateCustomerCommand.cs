using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands.Customers;

public record UpdateCustomerCommand(int Id, Customer Customer, User User) : ICommandDefinition;
