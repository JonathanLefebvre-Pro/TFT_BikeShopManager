using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands;

public record UpdateCustomerCommand(int Id, Customer Customer, User User) : ICommandDefinition;
