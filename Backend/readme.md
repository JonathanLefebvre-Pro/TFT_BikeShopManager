__Subprojects :__
1. BikeShopManager.WebApi (Startup project)
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

## 📋 Fonctionnalitées

### En cours de développement
1. Gestion des clients
2. Gestion des utilisateurs (employés), des roles et permissions

### A développer
1. Gestion du stock
2. Gestion des commandes
3. __**Fonctionnalitée a définir**__

## 🗄️ Base de Données

### Exigences minimales
- **5 tables minimum** (hors tables de jointure)
- Clés primaires et étrangères appropriées
- Timestamps (CreatedDate, UpdatedDate)
- Soft deletes ou archivage si pertinent

## 🔐 Sécurité

### Authentification JWT
- Tokens d'accès et refresh tokens
- Expiration configurable
- Support de Keycloak **OU** ASP.NET Core Identity
- Signature et validation des tokens

### Autorisation
- Minimum 3 rôles différents
- Contrôle d'accès basé sur les rôles (RBAC)
- Vérification des droits de propriété sur ressources
- Approbation/modération pour actions sensibles

### Protection Générale
- HTTPS en production
- CORS configuré
- Rate limiting
- Validation des entrées
- Logs d'audit

## 🚀 Endpoints REST

### Exigences minimales
- RESTful (GET, POST, PUT, DELETE, PATCH)
- Pagination et filtrage
- Tri et recherche
- Codes HTTP appropriés
- Réponses JSON cohérentes

## 📦 Architecture Technique

### Stack Requis
- **Framework** : ASP.NET Core 10
- **ORM/Accès données** : Entity Framework Core 10 **OU** Dapper
- **Base de données** : SQL Server
- **Authentification** : Keycloak OU ASP.NET Core Identity
- **Validation** : FluentValidation ou Data Annotations
- **Documentation** : Swagger/Scalar/OpenAPI
- **Serialization** : JSON

## 📋 Validation et Gestion des Erreurs

- Validation côté serveur obligatoire
- DTOs séparés pour Create/Update/Response
- Messages d'erreur explicites
- Middleware d'exception global
- Codes HTTP appropriés
- Logging centralisé

---

## ⚠️ Points Importants

- **Thème libre** : Choisir un domaine qui vous intéresse et qui permet une complexité suffisante
- **Minimum technique** : Respecter les 5 tables + 3 tables de jointure
- **Sécurité obligatoire** : JWT + rôles + autorisation
- **API REST complète** : 25+ endpoints fonctionnels
- **Qualité de code** : Tests, logs, gestion d'erreurs
- **Documentation** : Swagger + README + Guide déploiement