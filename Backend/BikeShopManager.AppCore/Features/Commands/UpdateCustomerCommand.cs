using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands;

public record UpdateCustomerCommand(int Id, User User, Customer Customer) : ICommandDefinition;
