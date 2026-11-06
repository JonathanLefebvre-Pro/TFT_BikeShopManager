# Project : Backend - WebApi

## Structure

1. Controllers
2. Dtos
3. Extensions
4. Mappers
5. Properties
6. Seeders

## Controlleurs

Les controlleurs sont les point d'entrée de l'API.

L'AuthController contient les endpoints d'authentification (Login, Logout, Refresh) qui n'ont pas besoin d'un role pour être accessibles ni d'être authentifié.

L'AuthUserController gère les accès pour les données relative à l'utilisateur authentifié. Par exemple, récuperer ses informations de profil et changer son mot de passe. L'utilisateur à besoin d'être authentifié, n'a pas besoin de rôle ou permission spécifique, mais sera identifié via son token.

L'EmployeeController permet l'accès aux données des employés. Seul les utilisateur authentifiés et disposant du role et des permissions suffisantes sont autorisé à y faire appel.

Le CustomerController permet la gestion des données clients et est accessible aux utilisateurs authentifiés et disposant du rôle et ou des permissions adéquates à chaque endpoint.

Le RoleController gère l'accès aux données de rôles et n'est accessible qu'aux utilisateurs authentifiés et disposant du rôle et ou des permissions adéquates.

## DTO

Les DTO sont les modèles de données d'entrée ou de sortie de l'API.
Un DTO est converti en Json par le backend et envoyé vers le client.
Quand le backend reçoit un Json en retour, il converti les données du body sous forme de DTO.

## Extensions

Connection : Ajout des options et de la configuration de connection.

Controller : Méthodes utilitaires pour ControllerBase. (Classe de base pour les controlleurs)

Cors : Configuration du système CORS. (Cross Origin Resource Sharing)

Jwt : Ajout des options et de la configuration pour le Json Web Token.

Seeding : Ajout des options et des services pour le seeding de données au démarrage de l'application.

## Mappers

Classes utilitaires permettants la conversion de DTO en Entité ou l'inverse.

## Properties

Json contenant les settings de démarrage de l'application.

## Seeders

Services permettant l'injection de données dans la DB au démarrage de l'application.