# 🎯 Projet DotNet 10 : API REST Sécurisée (Thème Libre)

## 📌 Contexte du Projet

Développer une **API REST** complète avec authentification JWT sécurisée pour un domaine métier de votre choix. Le sujet doit permettre la gestion de ressources, d'utilisateurs avec différents rôles, et favoriser les interactions entre utilisateurs.

### 🎨 Thème
- Plateforme de gestion commerciale d'un magasin de vélo

---

## 📋 Exigences Fonctionnelles

Le projet doit comporter au minimum 5 fonctionnalités majeurs liées à votre thème :

### Fonctionnalité 1
Gestion des clients

### Fonctionnalité 2
Gestion des utilisateurs (employés) et des roles

### Fonctionnalité 3
Gestion du stock

### Fonctionnalité 4
Gestion des commandes

### Fonctionnalité 5
Description générale des interactions/relations entre utilisateurs et ressources

---

## 🗄️ Structure de Base de Données

### Exigences minimales
- **5 tables minimum** (hors tables de jointure)
- Clés primaires et étrangères appropriées
- Timestamps (CreatedDate, UpdatedDate)
- Soft deletes ou archivage si pertinent

---

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

---

## 🚀 Endpoints REST

### Exigences minimales
- RESTful (GET, POST, PUT, DELETE, PATCH)
- Pagination et filtrage
- Tri et recherche
- Codes HTTP appropriés
- Réponses JSON cohérentes

---

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