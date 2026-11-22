using BikeShopManager.AppCore.Features.Options;
using BikeShopManager.AppCore.Features.Vault;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.AppRole;

namespace BikeShopManager.WebApi.Extensions;

public static class VaultExtensions
{
    public static WebApplicationBuilder BuildVaultConfiguration(this WebApplicationBuilder builder)
    {
        string? roleId =
            builder.Configuration["BAO_ROLE_ID"]
            ?? throw new InvalidOperationException("BAO_ROLE_ID manquant");
        string? secretId =
            builder.Configuration["BAO_SECRET_ID"]
            ?? throw new InvalidOperationException("BAO_SECRET_ID manquant");
        string? baoAddr =
            builder.Configuration["BAO_ADDR"]
            ?? throw new InvalidOperationException("BAO_ADDR manquant");
        IAuthMethodInfo authMethod = new AppRoleAuthMethodInfo(roleId, secretId);
        VaultClientSettings vaultClientSettings = new VaultClientSettings(baoAddr, authMethod);
        IVaultClient client = new VaultClient(vaultClientSettings);
        builder.Services.AddSingleton<IVaultClient>(client);

        builder.Configuration.Sources.Add(
            new VaultConfigurationSource(
                client,
                [new("bikeshopmanager/jwt", JwtOptions.SectionName)]
            )
        );

        builder.Configuration.Sources.Add(
            new VaultConfigurationSource(
                client,
                [new("bikeshopmanager/db", ConnectionStringsOptions.SectionName)]
            )
        );

        return builder;
    }
}
