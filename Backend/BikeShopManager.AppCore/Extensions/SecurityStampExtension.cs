using System;

namespace BikeShopManager.AppCore.Extensions;

public static class SecurityStampExtension
{
    /// <summary>
    /// Generate new SecurityStamp (For JWT Claims)
    /// </summary>
    /// <returns>SecurityStamp</returns>
    public static string GenerateSecurityStamp()
    {
        Guid guid = Guid.NewGuid();
        return guid.ToString("N");
    }
}
