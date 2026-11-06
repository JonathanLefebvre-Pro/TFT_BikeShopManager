## Project : Backend - AppCore

## Structure

1. Extensions
2. Features
    - Commands
    - Queries
    - Tokens
    - Results
    - Options
    - Errors
    - Dispatcher
    - Connection
3. Interfaces
    - Commands
    - Queries
    - Repositories
    - Tokens
    - Dispatcher
    - Connection
    - Behaviors (Pas encore implémenté ni utilisé !)

## Extensions

- DependencyInjection : Utilitaires pour l'ajour des services via l'injection de dépendances.
- PasswordHash : Utilitaires pour le hashing et la vérification des password.
- SecurityStamp : Utilitaire permettant de generer un SecurityStamp.
- UserIdentity : Utilitaires pour la vérification et la comparaison de données utilisateur.

## Features

- Commands : Commandes CQS.
- Queries : Requêtes CQS.
- Tokens : Services et utilitaires pour le JWT.
- Results : Implémentation du Result pattern.
- Options : Modèles de données pour l'Option pattern.
- Errors : Modèles de données pour l'Error pattern.
- Dispatcher : Implémentation du service Dispatcher et de ses des decorateurs, permettant l'envoie de commandes et requêtes CQS.
- Connection : Implémentation du DbSession service permettant la gestion transactionnelle d'actions comme les commandes et requêtes CQS via le TransactionalDispatcherDecorator.

## Interfaces

- Commands : Interfaces de base pour le système de commandes CQS.
- Queries : Interfaces de base pour le système de requêtes CQS.
- Repositories : Interfaces des repositories qui sont implémentés dans la couche Infrastructure.
- Tokens : Interfaces pour les utilitaires et services JWT.
- Dispatcher : Interfaces pour le service Dispatcher.
- Connection : Interface pour le service DbSession.
- Behaviors : Interface pour le système de behaviors. (Pas encore fonctionnel !)