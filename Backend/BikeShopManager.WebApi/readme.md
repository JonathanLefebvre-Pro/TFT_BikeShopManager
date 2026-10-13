# WebApi Database Description

## Informations générales

    Database : TFT_BikeShopManager_WebApi
    ORM : Dapper
    Backend : ASP .Net 10 Core
    Système de database : SqlServer 2019

## Conventions

    Id : INT IDENTITY(1,1)
    DateAjout : DATETIME2 DEFAULT SYSUTCDATETIME()
    DateMiseAJour : DATETIME2 DEFAULT SYSUTCDATETIME()
    Montants : DECIMAl(12,2) et TVA DECIMAL(5,2)
    Status : NVARCHAR(20) avec contrainte CHECK sur les valeurs listées, mappés en enum C#.

## Features

    1. Authentification et rôles : Connexion des employés (JWT), autorisations par rôle. Ex : un vendeur ne peut modifier que ses propres devis et commandes.
    2. Catalogue et stock : Gestion des articles, modèles de vélos et leurs exemplaires.
    3. Clients - Création et recherche de clients.
    4. Devis -> Commande -> Bon de livraison : Confirmation d'un devis avec réservation d'un vélo, création du bon de livraison.
    5. Facturation et paiement : Création de la vente et de la facture depuis le bon de livraison, numérotation continue, facture immuable, acomptes et paiement final.
    6. (Bonus) Vente comptoire d'accessoires : Vente sans commande ni facture avec modification du stock

    INFOS : 
    - L'ajout d'images n'ajoute rien pour l'instant à la démonstration.
    - L'envoi d'un emaikl au client peut $etre simulé via l'utilisation d'un faux service qui écrit dans les logs.

## Magasin

### Tables du Schéma Personnes

    Personnes.Utilisateurs - Liste de toutes les personnes, clients et employés confondus 
        - Id (PK Identity)
        - Nom (Non Null)
        - Prénom (Non Null)
        - Mail (Null, UQ, Obligatoire pour un employé car c'est son identifiant de connexion)
        - Tel (Null)
        - Adresse (Null)
        - Ville (Null)
        - Code postal (Null)
        - Pays (Null)
        - Date de naissance (Null)
        - Accepte de recevoir des infos marketing (Bit Non Null Defaut 0)
        - Date d'ajout
        - Date de mise à jour

    Personnes.Clients - Liste des utilisateurs étants des clients
        - Id (PK)
        - Id utilisateur (FK UQ Non Null)
        - Numéro de TVA (Null, pour les clients pro)
        - Est actif (Bit Non Null defaut 1) (Pour archiver un client)
        - Date d'ajout
        - Date de mise à jour

    Personnes.Employés - Liste des utilisateurs étants des employés
        - Id (PK)
        - Id utilisateur (FK UQ Non Null)

        INFO Utiliser Identity ou KeyCloak définit les champs d'authentification
        - Id du role (Admin, Manager, Vendeur, Mécanicien)
        - Login (UQ, basé sur l'email ?)
        - Mot de passe (Hashé)
        
        SI Identity (Pas d'id de role et de login, les rôles passent par les tables Roles et EmployeRoles)
        - Hash du mot de passe (Non Null)
        - Security stamp (NVARCHAR(64) Non Null)
        - Access failed count (Int Non Null defaut 0)
        - Lockout fin (DatetimeOffset Null)

        - Date d'embauche (Non Null)
        - Date de fin de contrat (Null)
        - Est-ce que l'employé est actif (bit) (Pour garder les anciens employés !)
        - Date d'ajout
        - Date de mise à jour

    Personnes.Roles - Liste des roles (Admin, Manager, Vendeur, Mécanicien, ...)
        - Id (PK)
        - Nom (Non Null, UQ)
        - Nom normalisé (Non Null UQ)
        - Date d'ajout
        - Date de mise à jour
    
    Personnes.EmployéRoles
        - Id de l'employé (Non Null)
        - Id du role (Non Null)
        - Clé primaire composite (EmployeId, RoleId)
        - Date d'ajout
        - Date de mise à jour

    Personnes.RefreshTokens
        - Id (PK)
        - Id de l'employé (Non Null)
        - Hash du token (Non Null)
        - Date d'expiration (Non Null)
        - Date de révocation (Nullable)
        - RemplaceParTokenHash (Non Nul) (Rotation des tokens)
        - Date d'ajout (Non Null)


### Tables du Schéma Stock

    Stock.TauxTva
        - Id (PK)
        - Libellé (Non Null) (Standard, alimentaire, main d'oeuvre, ...)
        - Taux (Non Null) (6, 21, ...)

    Stock.Marques - Liste des marques d'articles
        - Id (PK)
        - Nom (UQ Non Null)
        - Date d'ajout
        - Date de mise à jour

    Stock.Catégories - Liste des différentes catégories d'articles
        - Id (PK)
        - Nom (Non Null)
        - Description (Null)
        - Id de la catégorie parent (FK Null)
        - Domain (Non Null) (Article ou Vélo)
        - Date d'ajout
        - Date de mise à jour

    Stock.Emplacements - Réserve, Showroom, ...
        - Id (PK)
        - Nom (UQ Non Null)

    Stock.Images
        - Id (PK)
        - Id de l'article (Null) ??
        - Id du vélo (Null) ??
        - Url (Non Null)
        - Nom de l'image (Non Null) (Au cas ou l'image n'apparait pas)
        - Date d'ajout
        - Date de mise à jour
        **Contrainte** : L'un des deux champs (ArticleId/VeloId) doit être renseigné.

    Stock.Articles - Liste des accessoires dans le stock du magasin
        - Id (PK)
        - Nom (Non Null)
        - Id de la marque (Non Null)
        - Description (Null)
        - Id de la catégorie d'article (Non Null)
        - Prix d'achat (HTVA) (Non Null)
        - Prix de vente (HTVA/TVAC ?) (Non Null)
        - Id du taux de TVA (Non Null) (Diffère en fonction du type d'article, nouritture 6%, vêtements ou autres 21%)
        - Quantité disponible (Non Null defaut 0)
        - Quantité min (Non Null defaut 0)
        - Quantité max (Null)
        - EAN (Null, Index unique filtré)
        - Date d'ajout
        - Date de mise à jour

    Stock.VélosModèles - Liste des modèles de vélos dans le stock du magasin
        - Id (PK)
        - Nom (Non Null)
        - Id de la marque (Non Null)
        - Modèle (Non Null)
        - Description (Null)
        - Id de la catégorie de vélo (Non Null)
        - Prix d'achat (HTVA) (Non Null)
        - Prix de vente (HTVA/TVAC ?) (Non Null)
        - Id du taux de TVA (Non Null)
        - EAN (Null, UQ, Index unique filtré ???)
        - Est actif (bit Non Null defaut 1)
        - Date d'ajout
        - Date de mise à jour

    Stock.VélosExemplaires - Liste des vélos physiques dans le stock du magasin
        - Id (PK)
        - Id du modèle de vélo (FK Non Null)
        - Numéro de série (UQ Non Null)
        - Id de l'emplacement (Non Null) (Réserve, Showroom, ...)
        - Status ou Id du status ? (Non Null) (En stock, Reserve, Vendu)
        - Date d'ajout
        - Date de mise à jour

### Tables du Schéma Magasin

    Magasin.Commandes - Fiches de commande d'un client (Devis=Commande avec status Devis)
        - Id (PK)
        - Id du client (Non Null)
        - Id de l'employé en charge de la commande (Non Null)
        - Status (Non Null) (Devis, Confirmée, Facturée, Annulée, Expirée)
        - Numéro de devis (UQ, Non Null)
        - Numéro de commande (Null, Index unique filtré)
        - Date de validité du devis (Null)
        - Date de confirmation (Null)
        - Date d'ajout
        - Date de mise à jour
    
    Magasin.CommandeLignes - Lignes d'une commande
        - Id (PK)
        - Id de la commande (Non Null)
        - Type de ligne (Non Null) (Article, vélo, main d'oeuvre ?)
        - Id de l'article (Null)
        - Id du vélo (Null)
        - Id de l'exemplaire physique du vélo (Null) (renseigné à la confirmation, quand le vélo est réservé)
        - Libellé (Null) (obligatoire pour la main d'oeuvre)
        - Quantité (Non null, CHECK > 0)
        - Prix unitaire (HTVA) (Non Null)
        - Id du taux de TVA (Non Null)
        - Status de l'article (Non Null) (En stock, en commande, inconnu, ...)
        **Check** selon le type de ligne : Article exige l'id de l'article, Vélo exige l'id du vélo et Main d'oeuvre exige le libellé sans id d'article ni de vélo.

    Magasin.BonLivraison - Bons de livraison d'un client
        - Id (PK)
        - Id de la commande (UQ, Non Null)
        - Status (Non Null) (Pret, Facturé, Annulé)
        - Date de notification au client (Null)
        - Date d'ajout
        - Date de mise à jour

    Magasin.Ventes
        - Id (PK)
        - Id du client (Null) (normalement nullable mais devient obligatoire lors de la vente d'un vélo, une facture et une garantie ont besoin d'un client identifié.)
        - Id de l'employé en charge de la vente (Non Null)
        - Id du bon de livraison (Null, Index unique filtré)
        - Status (Non Null) (En cours, Payée, Annulée)
        - Total HTVA (Non Null)
        - Total TVA (Non Null)
        - Total TVAC (Non Null)
        - Date de la vente (Non Null)
        - Date d'ajout
        - Date de mise à jour

    **Info** : 
        - Une vente qui contient une ligne avec l'id d'un exemplaire de vélo (voir ci-dessous) ne peut pas passer au status 'Payé' tant qu'aucune facture n'y est rattachée.
        - Une vente qui contient un vélo doit avoir un id client
        - Le status de la ligne VeloExemplaire (physique) passe à 'Vendu' uniquement à l'émission de la facture et pas à la création de la vente.
    
    Magasin.VenteLignes - Lignes d'une vente
        - Id (PK)
        - Id de la vente (Non Null)
        - Libellé (Non Null)
        - Quantité (Non Null, CHECK < 0>)
        - Prix unitaire HTVA (Non Null)
        - Id du taux de TVA ou copie du taux ? (Non Null)
        - Id de l'article (Null)
        - Id d'un vélo (physique) (Null) (Un vélo ne peut pas être vendu hors facture)

    Magasin.Factures - Factures d'un client
        - Id (PK)
        - Numéro de facture (UQ, Non Null)
        - Status (Non Null) (Emise, payée, annulée)
        - Date d'émission (Non Null)
        - Date d'ajout
        - Date de mise à jour
        - INFO : Retirer le droit DELETE

    Magasin.paiements - Paiements d'un client
        - Id (PK)
        - Id de la vente (Null)
        - Id d'une commande (Null) (Pour acomptes)
        - Montant (Non Null, CHECK > 0)
        - Mode de paiement (Non Null) (Espèces, Carte, Virement, Autre)
        - Date de paiement (Non Null)
    **Contrainte** : L'un des deux champs (VenteId/CommandeId) doit être renseigné.

    **Info** : Les commandes sont converties en bon de livraison puis en factures. Une facture ne peux pas être supprimée.

    **Info** : Le total d'une ligne et d'une commande ne sont pas stockés mais calculé (quantité * prix)

    **Info** : Le montant déja payé et le reste à payer sont également calculés sur base de la somme des paiements

    Magasin.AuditLogs - Journal des actions importantes
        - Id (PK)
        - Id de l'employé (Null) Auteur de l'action
        - Action effectuée (Non Null) (Confirmation d'une commande, Emission d'une facture, Annulation d'une facture, ...)
        - Entitée (Non Null) (L'objet concerné, par exemple le commande 42)
        - Id de l'entitée (Null)
        - Details (Null) Json ?
        - Adresse ip (Null)
        - Date de l'action (Non Null, Defaut SysUTCDatetime())

### Audit Logs
    C'est le journal des actions importantes : qui a fait quoi, sur quel objet, et quand. 
    Si une facture est annulée ou un vélo disparaît de la réserve, on peut retrouver l'auteur et le moment. 
    La consigne demande des « logs d'audit », et c'est aussi un argument concret pour le jury, car le Manager peut contrôler l'activité de son équipe.

### Règles métier

- Un devis confirmé réserve les exemplaires d'un vélo (Status Réservé).
- Un devis expiré ou annulé libère le status d'un exemplaire de vélo.
- Un bon de livraison se crée uniquement depuis une commande Confirmée, puis le client est prévenu.
- Facturer depuis un bon de livraison crée la **"Vente"**, ses lignes et la facture.
- Le bon passe à **"Facturé"**, la commande à **"Facturée"**et les exemplaires à **"Vendu"**.
- Une vente contenant un vélo exige l'id du bon de livraison et l'id du client, et ne passe à **"Payée"** qu'avec une facture.
- Les acomptes sont déduits du solde au paiement final.
- Totaux de ligne, totaux de commande, montant payé et reste à payer sont calculés, jamais stockés. (Sauf les totaux de Ventes ?)
- Chaque action sensible écrit une entrée dans AuditLogs, dans la même transaction.
- Une sous-catégorie doit avoir le même Domain que son parent.


### Trucs en plus

- Note de crédit (Pour corriger une facture émise)
- Fournisseurs et commandes fournisseurs
- Mouvement de stock détaillés
- Garanties, retours et atelier
- Livraisons partielles