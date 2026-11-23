using VaultSharp;

namespace BikeShopManager.AppCore.Extensions;

public class VaultExtensions
{
    public static async Task<IDictionary<string, object>> GetSecret(
        IVaultClient vaultClient,
        string path,
        string mountPoint = "secret"
    )
    {
        var secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(
            path: path,
            mountPoint: mountPoint
        );
        return secret.Data.Data;
    }
}
