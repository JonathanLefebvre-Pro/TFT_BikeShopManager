# Projet : Backend

## Structure
1. BikeShopManager.WebApi (Projet de démarrage)
    - [Details du projet](./BikeShopManager.WebApi/readme.md)
2. BikeShopManager.Infrastruture
    - [Details du projet](./BikeShopManager.Infrastructure/readme.md)
    - [Database description](./BikeShopManager.Infrastructure/Database/readme.md)
3. BikeShopManager.Infrastructure.Test
    - [Details du projet](./BikeShopManager.Infrastructure.Test/readme.md)
4. BikeShopManager.AppCore
    - [Details du projet](./BikeShopManager.AppCore/readme.md)
5. BikeShopManager.Domain
    - [Details du projet](./BikeShopManager.Domain/readme.md)

## Fonctionnalitées et status

### En cours de développement
1. Gestion des clients
2. Gestion des utilisateurs (employés), des roles et permissions

### A développer
1. Gestion du stock
2. Gestion des commandes
3. __**Fonctionnalitée a définir**__

## Work with DevContainer

1. In VS Code, run **Reopen in Container** (or **Rebuild and Reopen in Container**).
2. Unseal the Vault
3. Then start the app with :

```bash
dotnet watch run --project BikeShopManager.WebApi --no-launch-profile
```
4. API : https://localhost:7105/scalar


## Save appSettings.Development.json
"JwtSettings": {
    "Secret": "2141ce53a8880b92984a4d2496f3c2bcff5354757e30b3a4566cda137558d25a",
    "Issuer": "JwtApp",
    "Audience": "JwtAppUsers",
    "ExpirationInMinutes": 15,
    "RefreshTokenDays": 7
  },
"ConnectionStrings": {
    "DefaultConnection": "Data Source=JONATHAN\\DATAVIZ;Initial Catalog=TFT_BikeShopManager_WebApi;Integrated Security=True;Trust Server Certificate=True",
    "TechnofuturConnection" : "Data Source=GOS-VDI406\\TFTIC;Initial Catalog=TFT_BikeShopManager_WebApi;Integrated Security=True;Trust Server Certificate=True",
    "DockerConnection" : "Server=bikeshopmanager-db;Database=BikeShopManagerWebApp;User Id=sa;Password=Test1234=;Trust Server Certificate=True"
  },