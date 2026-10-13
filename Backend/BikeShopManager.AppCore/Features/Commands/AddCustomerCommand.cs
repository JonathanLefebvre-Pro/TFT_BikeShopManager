using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands;

public record AddCustomerCommand(User User, Customer Customer) : ICommandDefinition<int>;
