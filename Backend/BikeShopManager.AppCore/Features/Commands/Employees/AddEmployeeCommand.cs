using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands.Employees;

public record AddEmployeeCommand(Employee Employee, User User, int RoleId)
    : ICommandDefinition<int>;
