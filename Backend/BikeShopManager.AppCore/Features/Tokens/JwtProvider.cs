using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BikeShopManager.AppCore.Extensions;
using BikeShopManager.AppCore.Features.Options;
using BikeShopManager.AppCore.Interfaces.Tokens;
using BikeShopManager.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using VaultSharp;

namespace BikeShopManager.AppCore.Features.Tokens;

public class JwtProvider(IOptions<JwtOptions> Options, IVaultClient VaultClient, bool useVault)
    : IJwtProvider
{
    public async Task<JwtAccessToken> GenerateAccessToken(Employee employee)
    {
        if (employee is null || employee.User is null)
            throw new ArgumentException("Invalid user");

        JsonWebTokenHandler tokenHandler = new JsonWebTokenHandler();
        string? jwtSecret = string.Empty;
        string? jwtIssuer = string.Empty;
        string? jwtAudience = string.Empty;
        int jwtAccessExpiration = 0;

        if (useVault)
        {
            IDictionary<string, object> secrets = await VaultExtensions.GetSecret(
                VaultClient,
                "bikeshopmanager/jwt"
            );
            jwtSecret = (string)secrets["secret"];
            jwtIssuer = (string)secrets["issuer"];
            jwtAudience = (string)secrets["audience"];
            jwtAccessExpiration = int.Parse((string)secrets["ExpirationInMinutes"]);
        }
        else
        {
            jwtSecret = Options.Value.Secret;
            jwtIssuer = Options.Value.Issuer;
            jwtAudience = Options.Value.Audience;
            jwtAccessExpiration = Options.Value.ExpirationInMinutes;
        }

        byte[] key = Encoding.UTF8.GetBytes(jwtSecret!);

        List<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, employee.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, employee.User.Email!),
            new Claim(
                JwtRegisteredClaimNames.Name,
                $"{employee.User.FirstName}_${employee.User.LastName}"
            ),
            new Claim(
                JwtRegisteredClaimNames.Iat,
                DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()
            ),
            new Claim(ClaimTypes.Role, employee.Role.Name),
            new Claim("SecurityStamp", employee.SecurityStamp),
        };

        DateTime accessExpirationTime = DateTime.UtcNow.AddMinutes(jwtAccessExpiration);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = jwtIssuer,
            Audience = jwtAudience,
            Subject = new ClaimsIdentity(claims),
            Expires = accessExpirationTime,
            IssuedAt = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
        };

        JwtAccessToken token = new JwtAccessToken()
        {
            Token = tokenHandler.CreateToken(tokenDescriptor),
            ExpireAt = accessExpirationTime,
        };

        return token;
    }

    public async Task<JwtRefreshToken> GenerateRefreshToken()
    {
        int jwtRefreshExpiration = 0;
        if (useVault)
        {
            IDictionary<string, object> secrets = await VaultExtensions.GetSecret(
                VaultClient,
                "bikeshopmanager/jwt"
            );
            jwtRefreshExpiration = int.Parse((string)secrets["RefreshTokenDays"]);
        }
        else
        {
            jwtRefreshExpiration = Options.Value.RefreshTokenDays;
        }

        DateTime refreshExpirationTime = DateTime.UtcNow.AddDays(jwtRefreshExpiration);
        byte[] randomBytes = RandomNumberGenerator.GetBytes(64);

        JwtRefreshToken token = new JwtRefreshToken()
        {
            Token = Convert.ToBase64String(randomBytes),
            ExpireAt = refreshExpirationTime,
        };

        return token;
    }

    public string HashRefreshToken(string token)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
    }

    public static TokenValidationParameters GenerateTokenValidationObject(
        string? issuer,
        string? audience,
        string? secret
    )
    {
        if (
            string.IsNullOrWhiteSpace(issuer)
            || string.IsNullOrWhiteSpace(audience)
            || string.IsNullOrWhiteSpace(secret)
        )
            throw new ArgumentNullException();

        byte[] key = Encoding.UTF8.GetBytes(secret);
        return new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidIssuer = issuer,
            ValidAudience = audience,
            NameClaimType = JwtRegisteredClaimNames.Name,
            RoleClaimType = ClaimTypes.Role,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    }
}
