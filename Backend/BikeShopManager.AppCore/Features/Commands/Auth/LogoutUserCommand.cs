using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands;

public record LogoutUserCommand(string RefreshToken) : ICommandDefinition;
