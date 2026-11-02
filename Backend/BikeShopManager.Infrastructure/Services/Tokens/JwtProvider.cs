using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BikeShopManager.AppCore.Interfaces.Tokens;
using BikeShopManager.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BikeShopManager.Infrastructure.Services.Tokens;

public class JwtProvider(IConfiguration Configuration) : IJwtProvider
{
    public TokenResponse GenerateToken(Employee employee)
    {
        if (employee is null || employee.User is null)
            throw new ArgumentException("Invalid user");

        JsonWebTokenHandler tokenHandler = new JsonWebTokenHandler();
        IConfigurationSection jwtSettings = Configuration.GetSection("JwtSettings");
        string? jwtSecret = jwtSettings.GetValue<string>("Secret");
        string? jwtIssuer = jwtSettings.GetValue<string>("Issuer");
        string? jwtAudience = jwtSettings.GetValue<string>("Audience");
        int jwtAccessExpiration = jwtSettings.GetValue<int>("ExpirationInMinutes");
        int jwtRefreshExpiration = jwtSettings.GetValue<int>("RefreshTokenDays");
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
        };

        DateTime accessExpirationTime = DateTime.UtcNow.AddMinutes(jwtAccessExpiration);
        DateTime refreshExpirationTime = DateTime.UtcNow.AddDays(jwtRefreshExpiration);

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

        TokenResponse response = new TokenResponse()
        {
            AccessToken = tokenHandler.CreateToken(tokenDescriptor),
            AccessExpireAt = accessExpirationTime,
            RefreshToken = GenerateRefreshToken(),
            RefreshExpireAt = refreshExpirationTime,
        };

        return response;
    }

    public string GenerateRefreshToken()
    {
        byte[] randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }

    public string HashToken(string token)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
    }
}
