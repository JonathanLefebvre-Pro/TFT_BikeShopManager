using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands;

public record AddEmployeeCommand(User User, Employee Employee, int RoleId)
    : ICommandDefinition<int>;
