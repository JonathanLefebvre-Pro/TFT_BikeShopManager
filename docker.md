# BikeShopManager Docker Setup

Docker Compose setup with:
- ASP.NET Web API container
- MS SQL Server database container
- MS SQL init container
- OpenBao container (vault secrets management)

## Docker Compose
```bash
# Start (build) and stop the containers
docker compose up --build -d
docker compose down

### List running containers
docker compose ps

### Display configuration
docker compose config
```

## Generate certificates
```bash
dotnet dev-certs https -ep ./certs/aspnetapp.pfx -p <Password>
dotnet dev-certs https --trust
```

## OpenBao

### 1. Init & unseal

```bash
docker compose exec bikeshopmanager-vault bao operator init
docker compose exec bikeshopmanager-vault bao operator unseal
```
Run `unseal` repeatedly, with a different unseal key each time, until the vault is unsealed.

| Item               | Value                                           |
|--------------------|-------------------------------------------------|
| Unseal Key 1       | `kV99kZi/b/kmPsm/zu6iRXzutrOeWFtnc5PtOrBegEFI`  |
| Unseal Key 2       | `GJz6OrXwceyYxRFhYZiLfv2riNaOLSwKftTBBoT8YoiJ`  |
| Unseal Key 3       | `70mBF3Rj9+frOnF06SdihkY77IvgKsPPTktgaHWC95/8`  |
| Unseal Key 4       | `YBhGsHtXJAv6vHRGYEMg+tgnUEWA1it2qxOEY23LMqeu`  |
| Unseal Key 5       | `487BomMe0hq1AxIx0davL2LbiFYjqP/y/yaooTuK4WP6`  |
| Initial Root Token | `s.gn1bj6Q3RYC073vUNCYoZatM`                    |

### 2. Enable the KV engine

```bash
docker compose exec -e BAO_TOKEN=s.gn1bj6Q3RYC073vUNCYoZatM bikeshopmanager-vault \
  bao secrets enable -path=secret kv-v2
```

### 3. Create the policy
```bash
docker compose exec -e BAO_TOKEN=s.gn1bj6Q3RYC073vUNCYoZatM bikeshopmanager-vault bao secrets enable -path=secret kv-v2
```

### 4. Configure AppRole
```bash
docker compose exec -T -e BAO_TOKEN=s.gn1bj6Q3RYC073vUNCYoZatM bikeshopmanager-vault bao policy write bikeshopmanager-vault-read - < ./openbao/policies/bikeshopmanager-vault-read.hcl

docker compose exec -e BAO_TOKEN=s.gn1bj6Q3RYC073vUNCYoZatM bikeshopmanager-vault sh -c '
bao auth enable approle &&
bao write auth/approle/role/bikeshopmanager-api token_policies=bikeshopmanager-vault-read token_ttl=1h token_max_ttl=4h &&
bao read auth/approle/role/bikeshopmanager-api/role-id &&
bao write -f auth/approle/role/bikeshopmanager-api/secret-id'
```
The output provides the values to configure in the Web API:

| Field                | Value                                  |
|----------------------|----------------------------------------|
| `role_id`            | `64d20d3a-b597-f9f8-9004-45b741a18d33` |
| `secret_id`          | `a99f076b-bbda-dc84-9118-3034e83ed99e` |
| `secret_id_accessor` | `edcfb806-b604-9d3a-6ad5-e9fa9842d4d5` |
| `secret_id_num_uses` | `0`                                    |
| `secret_id_ttl`      | `0s`                                   |

## Work with DevContainer

1. In VS Code, run **Reopen in Container** (or **Rebuild and Reopen in Container**).
2. Then start the app:

```bash
dotnet watch run --project BikeShopManager.WebApi --no-launch-profile
```

## Import volume backup (PowerShell)

1. Put the backup file `bikeshopmanager-vault-data.tar.gz` in `C:\Temp`.
2. Create the volume and restore the backup:

```powershell
docker volume create bikeshopmanager-vault-data

docker run --rm `
  -v bikeshopmanager-vault-data:/data `
  -v "C:\Temp:/backup" `
  alpine tar xzf /backup/bikeshopmanager-vault-data.tar.gz -C /data
```

## VS Code user settings (JSON)

```json
{
  "dev.containers.executeInWSL": false,
  "dev.containers.mountWaylandSocket": false
}
```