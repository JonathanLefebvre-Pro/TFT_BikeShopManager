using BikeShopManager.AppCore.Interfaces.Commands;

namespace BikeShopManager.AppCore.Features.Commands.Auth;

public record LogoutUserCommand(string RefreshToken) : ICommandDefinition;
