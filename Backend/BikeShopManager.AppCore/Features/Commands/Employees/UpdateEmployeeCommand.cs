using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands;

public record UpdateEmployeeCommand(int Id, Employee Employee, User User, int RoleId)
    : ICommandDefinition;
