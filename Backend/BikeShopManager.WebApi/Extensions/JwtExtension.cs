using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BikeShopManager.WebApi.Extensions;

public static class JwtExtension
{
    public static WebApplicationBuilder ConfigureJwt(this WebApplicationBuilder builder)
    {
        IConfigurationSection jwtSettings = builder.Configuration.GetSection("JwtSettings");
        string? jwtSecret = jwtSettings.GetValue<string>("Secret");
        string? jwtIssuer = jwtSettings.GetValue<string>("Issuer");
        string? jwtAudience = jwtSettings.GetValue<string>("Audience");
        byte[] key = Encoding.UTF8.GetBytes(jwtSecret!);

        builder
            .Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //options.SaveToken = true;  ???
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    //RoleClaimType = ClaimTypes.Role,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });

        return builder;
    }
}
