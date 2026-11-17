using System.Text;
using BikeShopManager.AppCore.Features.Options;
using BikeShopManager.AppCore.Features.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using VaultSharp;

namespace BikeShopManager.WebApi.Extensions.Jwt;

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

    public static async Task<WebApplicationBuilder> BuildJwtConfiguration(
        this WebApplicationBuilder builder
    )
    {
        // JwtOptions? jwtOptions = builder
        //     .Configuration.GetSection(JwtOptions.SectionName)
        //     .Get<JwtOptions>();
        // string? jwtSecret = jwtOptions?.Secret;
        // string? jwtIssuer = jwtOptions?.Issuer;
        // string? jwtAudience = jwtOptions?.Audience;

        //Serilog.ILogger logger = Log.ForContext(typeof(JwtExtension));
        IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
        IVaultClient vaultClient = serviceProvider.GetRequiredService<IVaultClient>();
        IDictionary<string, object> secrets = await AppCore.Extensions.VaultExtensions.GetSecret(
            vaultClient,
            "bikeshopmanager-webapp/jwt"
        );
        var jwtS = secrets["secret"];
        var jwtI = secrets["issuer"];
        var jwtA = secrets["audience"];
        //logger.Debug((string)jwtS);

        byte[] key = Encoding.UTF8.GetBytes((string)jwtS!);

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
                    (string)jwtI,
                    (string)jwtA,
                    (string)jwtS
                );
                options.Events = new JwtBearerEventsValidator();
            });

        builder.Services.AddAuthorization();

        return builder;
    }
}
