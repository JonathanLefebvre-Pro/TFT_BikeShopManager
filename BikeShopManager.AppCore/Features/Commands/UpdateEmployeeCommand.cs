using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Commands;

public record UpdateEmployeeCommand(int Id, User User, Employee Employee) : ICommandDefinition;
