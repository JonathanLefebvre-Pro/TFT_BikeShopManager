using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BikeShopManager.AppCore.Features.Options;
using BikeShopManager.AppCore.Interfaces.Tokens;
using BikeShopManager.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BikeShopManager.AppCore.Features.Tokens;

public class JwtProvider(IOptions<JwtOptions> Options) : IJwtProvider
{
    public JwtAccessToken GenerateAccessToken(Employee employee)
    {
        if (employee is null || employee.User is null)
            throw new ArgumentException("Invalid user");

        JsonWebTokenHandler tokenHandler = new JsonWebTokenHandler();
        string? jwtSecret = Options.Value.Secret;
        string? jwtIssuer = Options.Value.Issuer;
        string? jwtAudience = Options.Value.Audience;
        int jwtAccessExpiration = Options.Value.ExpirationInMinutes;
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

    public JwtRefreshToken GenerateRefreshToken()
    {
        int jwtRefreshExpiration = Options.Value.RefreshTokenDays;
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
