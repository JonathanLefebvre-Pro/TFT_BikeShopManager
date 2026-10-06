# WebApi Database Description

## Informations générales

    Database : TFT_BikeShopManager_WebApi
    ORM : Dapper
    Backend : ASP .Net 10 Core
    Système de database : SqlServer 2019

## Magasin

### Tables

    Utilisateurs - Liste de tous les utilisateurs, clients et employés confondus 
        - Id
        - Nom
        - Prénom
        - Adresse
        - Tel
        - Mail
        - Date d'ajout
        - Date de mise à jour

    Utilisateurs.Clients - Liste des utilisateurs étants des clients :
        - Id
        - Id utilisateur
        - Date d'ajout
        - Date de mise à jour
        - Autres infos liées uniquement aux clients

    Utilisateurs.Employés - Liste des utilisateurs étants des employés :
        - Id 
        - Id utilisateur
        - Role (Admin, Manager, Employee, ...)
        - Mot de passe (Hashé)
        - Login (Basé sur l'email ?)
        - Date d'ajout
        - Date de mise à jour
        - **Autres infos liées uniquement aux employés**
#
    Magasin.Commandes - Fiches de commande d'un client :
        - Id
        - Liste d'articles (Accessoires et/ou vélos)
            - Quantité
            - Prix (TVAC/HTVA)
            - Taux TVA (21%)
            - Status de l'article (En stock, en commande, status inconnu, ...)
            - Montant total (quantité * prix)
        - Liste de main d'oeuvre **(En option)**
            - Titre
            - Taux TVA (6%)
        - Montant total à payer
        - Montant déja payé
        - Montant restant à payer
        - Status (Cloturée, annulée, ...)

    Magasin.BonLivraison - Bons de livraison d'un client :

    Magasin.Factures - Factures d'un client

    **Info** : Les commandes sont converties en bon de livraison puis en factures. Une facture ne peux pas être supprimée.
#
    Stock.Article - Liste des accessoires dans le stock du magasin
        - Id
        - Nom
        - Marque
        - Description
        - Type d'article
        - Sous type d'article
        - Prix (HTVA)
        - Quantité disponible
        - Quantité min
        - Quantité max
        - EAN

    Stock.Vélos - Liste des vélos dans le stock du magasin
        - Id
        - Nom
        - Marque
        - Modèle
        - Description
        - Type de vélo
        - Sous type de vélo
        - Prix (HTVA)
        - EAN
#
    Factures

## Atelier

### Tables

    Fiches de réparations