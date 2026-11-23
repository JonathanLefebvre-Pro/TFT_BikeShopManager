using Microsoft.Extensions.Configuration;
using VaultSharp;

namespace BikeShopManager.AppCore.Features.Vault;

public sealed record VaultSecretMapping(
    string SecretPath,
    string Section,
    string MountPoint = "secret"
);

public sealed class VaultConfigurationSource(
    IVaultClient client,
    IReadOnlyList<VaultSecretMapping> mappings
) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new VaultConfigurationProvider(client, mappings);
    }
}

public sealed class VaultConfigurationProvider(
    IVaultClient client,
    IReadOnlyList<VaultSecretMapping> mappings
) : ConfigurationProvider
{
    public override void Load() => LoadAsync().GetAwaiter().GetResult();

    private async Task LoadAsync()
    {
        Dictionary<string, string?> data = new Dictionary<string, string?>(
            StringComparer.OrdinalIgnoreCase
        );

        foreach (VaultSecretMapping m in mappings)
        {
            var secret = await client.V1.Secrets.KeyValue.V2.ReadSecretAsync(
                m.SecretPath,
                mountPoint: m.MountPoint
            );

            foreach (var (key, value) in secret.Data.Data)
                data[$"{m.Section}:{key}"] = value?.ToString();
        }

        Data = data;
    }
}
