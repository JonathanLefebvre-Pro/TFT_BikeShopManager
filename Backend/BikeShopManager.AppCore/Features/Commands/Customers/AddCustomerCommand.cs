using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands;

public record AddCustomerCommand(Customer Customer, User User) : ICommandDefinition<int>;
