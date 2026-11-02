using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BikeShopManager.AppCore.Features.Options;
using BikeShopManager.AppCore.Interfaces.Tokens;
using BikeShopManager.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BikeShopManager.Infrastructure.Services.Tokens;

public class JwtProvider(IOptions<JwtOptions> Options) : IJwtProvider
{
    public TokenResponse GenerateToken(Employee employee)
    {
        if (employee is null || employee.User is null)
            throw new ArgumentException("Invalid user");

        JsonWebTokenHandler tokenHandler = new JsonWebTokenHandler();
        string? jwtSecret = Options.Value.Secret;
        string? jwtIssuer = Options.Value.Issuer;
        string? jwtAudience = Options.Value.Audience;
        int jwtAccessExpiration = Options.Value.ExpirationInMinutes;
        int jwtRefreshExpiration = Options.Value.RefreshTokenDays;
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
