using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands.Employees;

public record UpdateEmployeeCommand(int Id, Employee Employee, User User, int RoleId)
    : ICommandDefinition;
