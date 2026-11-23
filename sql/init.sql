USE [master]

IF DB_ID(N'BikeShopManagerWebApp') IS NULL
    CREATE DATABASE BikeShopManagerWebApp;
GO

USE BikeShopManagerWebApp;
GO

:r init_Persons.sql
GO
:r init_Stock.sql
GO
-- :r init_Shop.sql
-- GO