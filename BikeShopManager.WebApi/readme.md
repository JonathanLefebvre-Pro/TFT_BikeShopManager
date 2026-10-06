# WebApi Database Description

## Informations générales

    Database : TFT_BikeShopManager_WebApi
    ORM : Dapper
    Backend : ASP .Net 10 Core
    Système de database : SqlServer 2019

## Magasin

### Tables

    Personnes.Utilisateurs - Liste de toutes les personnes, clients et employés confondus 
        - Id (PK)
        - Nom
        - Prénom
        - Adresse
        - Code postal
        - Ville
        - Pays
        - Date de naissance (opt)
        - Tel
        - Mail (UQ)
        - Accepte de recevoir des infos marketing (bit) (mail)
        - Date d'ajout
        - Date de mise à jour

    Personnes.Clients - Liste des utilisateurs étants des clients
        - Id (PK)
        - Id utilisateur (FK)
        - Numéro de TVA (opt, pour les clients pro)
        - Date d'ajout
        - Date de mise à jour
        - Autres infos liées uniquement aux clients

    Personnes.Employés - Liste des utilisateurs étants des employés
        - Id (PK)
        - Id utilisateur (FK)
        - Role (Admin, Manager, Vendeur, Mécanicien)
        - Login (UQ, basé sur l'email ?)
        - Mot de passe (Hashé)
        - Date d'embauche
        - Date de fin de contrat (opt)
        - Est-ce que l'employé est actif (bit) (Pour garder les anciens employés !)
        - Date d'ajout
        - Date de mise à jour
        - **Autres infos liées uniquement aux employés**
#
    Magasin.Commandes - Fiches de commande d'un client
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
        - Date d'ajout
        - Date de mise à jour

    Magasin.BonLivraison - Bons de livraison d'un client
        - Id
        - Date d'ajout
        - Date de mise à jour

    Magasin.Factures - Factures d'un client
        - Id
        - Date d'ajout
        - Date de mise à jour

    Magasin.Ventes - Liste des ventes
        - Id
        - Id de la facture
        - Id du client
        - Id de l'employé en charge de la vente
        - Total HTVA
        - Total TVA
        - Total TVAC


    **Info** : Les commandes sont converties en bon de livraison puis en factures. Une facture ne peux pas être supprimée.
#
    Stock.TauxTva
        - Id (PK)
        - Libellé (Standard, alimentaire, main d'oeuvre, ...)
        - Taux (6, 21, ...)

    Stock.Marques - Liste des marques d'articles
        - Id (PK)
        - Nom (UQ)
        - Date d'ajout
        - Date de mise à jour

    Stock.ImageArticle
        - Id (PK)
        - Id de l'article (Nullable) ??
        - Id du vélo (Nullable) ??
        - Url
        - Nom de l'image (Au cas ou l'image n'apparait pas)
        - Date d'ajout
        - Date de mise à jour

    Stock.Emplacements
        - Id
        - Nom

    Stock.ArticlesCatégories - Liste des différentes catégories d'articles
        - Id
        - Nom
        - Description
        - Date d'ajout
        - Date de mise à jour

    Stock.ArticlesSousCatégories - Liste des différentes sous catégories d'articles
        - Id
        - Nom
        - Description
        - Date d'ajout
        - Date de mise à jour

    Stock.VéloCatégories - Liste des différentes catégories d'articles
        - Id
        - Nom
        - Description
        - Date d'ajout
        - Date de mise à jour

    Stock.VéloSousCatégories - Liste des différentes sous catégories d'articles
        - Id
        - Nom
        - Description
        - Date d'ajout
        - Date de mise à jour

    Stock.Article - Liste des accessoires dans le stock du magasin
        - Id
        - Nom
        - Id de la marque
        - Description
        - Id de la catégorie d'article
        - Sous type d'article
        - Prix d'achat (HTVA)
        - Prix de vente (HTVA/TVAC)
        - Id du taux de TVA (Diffère en fonction du type d'article, nouritture 6%, vêtements ou autres 21%)
        - Quantité disponible
        - Quantité min
        - Quantité max
        - EAN
        - Date d'ajout
        - Date de mise à jour

    Stock.Vélos - Liste des vélos dans le stock du magasin
        - Id
        - Nom
        - Marque
        - Modèle
        - Description
        - Type de vélo
        - Sous type de vélo
        - Prix d'achat (HTVA)
        - Prix de vente (HTVA/TVAC)
        - TVA (21%)
        - EAN
        - Date d'ajout
        - Date de mise à jour
        - Id de l'emplacement (Réserve, Showroom, ...)

## Atelier

### Tables

    Fiches de réparations