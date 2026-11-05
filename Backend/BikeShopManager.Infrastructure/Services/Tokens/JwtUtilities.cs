using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BikeShopManager.AppCore.Interfaces.Tokens;

namespace BikeShopManager.Infrastructure.Services.Tokens;

public class JwtUtilities : IJwtUtilities
{
    public int GetClaimSub(ClaimsPrincipal principal)
    {
        //Get the employee id from the JWT claims
        string? employeeId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrWhiteSpace(employeeId))
            return 0;

        try
        {
            return int.Parse(employeeId);
        }
        catch (Exception error)
        {
            return 0;
        }
    }
}
