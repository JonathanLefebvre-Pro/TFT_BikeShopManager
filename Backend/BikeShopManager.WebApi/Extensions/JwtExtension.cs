using System.Text;
using BikeShopManager.AppCore.Features.Options;
using BikeShopManager.Infrastructure.Services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
                options.TokenValidationParameters = JwtProvider.GenerateTokenValidationObject(
                    jwtIssuer,
                    jwtAudience,
                    jwtSecret
                );
                options.Events = new JwtBearerEventsValidator();
            });

        builder.Services.AddAuthorization();

        return builder;
    }
}
