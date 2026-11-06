using System.Security.Claims;

namespace BikeShopManager.AppCore.Interfaces.Tokens;

public interface IJwtUtilities
{
    int GetClaimSub(ClaimsPrincipal principal);
}
