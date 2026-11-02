using System.Data;
using System.Security.Claims;
using System.Text;
using BikeShopManager.AppCore.Features.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BikeShopManager.WebApi.Extensions;

public static class JwtExtension
{
    public static IServiceCollection AddJwtOptions(this IServiceCollection services)
    {
        services
            .AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddConnectionStringsOptions(this IServiceCollection services)
    {
        services
            .AddOptions<ConnectionStringsOptions>()
            .BindConfiguration(ConnectionStringsOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static WebApplicationBuilder AddJwtConfiguration(this WebApplicationBuilder builder)
    {
        JwtOptions? jwtOptions = builder
            .Configuration.GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>();
        string? jwtSecret = jwtOptions?.Secret;
        string? jwtIssuer = jwtOptions?.Issuer;
        string? jwtAudience = jwtOptions?.Audience;
        byte[] key = Encoding.UTF8.GetBytes(jwtSecret!);

        builder
            .Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    NameClaimType = JwtRegisteredClaimNames.Name,
                    RoleClaimType = ClaimTypes.Role,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });

        builder.Services.AddAuthorization();

        return builder;
    }

    public static WebApplicationBuilder AddConnectionStringConfiguration(
        this WebApplicationBuilder builder
    )
    {
        ConnectionStringsOptions? connectionStringsOptions = builder
            .Configuration.GetSection(ConnectionStringsOptions.SectionName)
            .Get<ConnectionStringsOptions>();

        builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(
            connectionStringsOptions?.TechnofuturConnection
        ));

        return builder;
    }
}
