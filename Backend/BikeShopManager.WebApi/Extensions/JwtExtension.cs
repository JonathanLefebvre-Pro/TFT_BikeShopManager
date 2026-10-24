using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BikeShopManager.WebApi.Extensions;

public static class JwtExtension
{
    private const string jwtSecret =
        "2141ce53a8880b92984a4d2496f3c2bcff5354757e30b3a4566cda137558d25a";
    private const string jwtIssuer = "JwtApp";
    private const string jwtAudience = "JwtAppUsers";

    public static IServiceCollection ConfigureJwt(this IServiceCollection services)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //options.SaveToken = true;  ???
                byte[] key = Encoding.UTF8.GetBytes(jwtSecret);
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });

        return services;
    }
}
