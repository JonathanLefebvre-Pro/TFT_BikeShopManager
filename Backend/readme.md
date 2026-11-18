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
2. Then start the app:

```bash
dotnet watch run --project BikeShopManager.WebApi --no-launch-profile
```