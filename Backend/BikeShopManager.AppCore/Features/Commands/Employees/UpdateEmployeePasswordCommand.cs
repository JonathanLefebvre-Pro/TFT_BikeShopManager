using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands.Employees;

public record UpdateEmployeePasswordCommand(int Id, string OldPassword, string NewPassword)
    : ICommandDefinition;
