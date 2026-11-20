USE BikeShopManagerWebApp;
GO

CREATE SCHEMA Stock;

GO

CREATE TABLE Stock.VatRates (
    Id INT NOT NULL IDENTITY(1,1),
    Label NVARCHAR(50) NOT NULL,
    Rate DECIMAL(5, 2) NOT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_VatRates_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_VatRates_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_VatRates PRIMARY KEY (Id),
    CONSTRAINT UQ_VatRates_Label UNIQUE (Label),
    CONSTRAINT CK_VatRates_Rate CHECK (Rate >= 0 AND Rate <= 100)
);

GO

CREATE TABLE Stock.Brands (
    Id INT NOT NULL IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Brands_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Brands_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Brands PRIMARY KEY (Id),
    CONSTRAINT UQ_Brands_Name UNIQUE (Name)
);

GO

CREATE TABLE Stock.Categories (
    Id INT NOT NULL IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255) NULL,
    ParentCategoryId INT NULL,
    Domain NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Categories_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Categories_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Categories PRIMARY KEY (Id),
    CONSTRAINT UQ_Categories_Domain_ParentCategoryId_Name UNIQUE (Domain, ParentCategoryId, Name),
    CONSTRAINT FK_Categories_ParentCategories FOREIGN KEY (ParentCategoryId) REFERENCES Stock.Categories(Id),
    CONSTRAINT CK_Categories_Domain CHECK (Domain IN ('Article', 'Bike')),
    CONSTRAINT CK_Categories_NotOwnParent CHECK (ParentCategoryId IS NULL OR ParentCategoryId <> Id)
);

GO

CREATE TABLE Stock.Locations (
    Id INT NOT NULL IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Locations_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Locations_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Locations PRIMARY KEY (Id),
    CONSTRAINT UQ_Locations_Name UNIQUE (Name)
);

GO

CREATE TABLE Stock.Articles (
    Id INT NOT NULL IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    BrandId INT NOT NULL,
    Description NVARCHAR(255) NULL,
    CategoryId INT NOT NULL,
    PurchasePriceExclVat DECIMAL(10, 2) NOT NULL,
    SalePriceInclVat DECIMAL(10, 2) NOT NULL,
    VatRateId INT NOT NULL,
    AvailableQuantity INT NOT NULL CONSTRAINT DF_Articles_AvailableQuantity DEFAULT 0,
    MinQuantity INT NOT NULL CONSTRAINT DF_Articles_MinQuantity DEFAULT 0,
    MaxQuantity INT NULL,
    EAN NVARCHAR(13) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Articles_IsActive DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Articles_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Articles_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Articles PRIMARY KEY (Id),
    CONSTRAINT FK_Articles_Brands FOREIGN KEY (BrandId) REFERENCES Stock.Brands(Id),
    CONSTRAINT FK_Articles_Categories FOREIGN KEY (CategoryId) REFERENCES Stock.Categories(Id),
    CONSTRAINT FK_Articles_VatRates FOREIGN KEY (VatRateId) REFERENCES Stock.VatRates(Id),
    CONSTRAINT CK_Articles_Prices CHECK (PurchasePriceExclVat >= 0 AND SalePriceInclVat >= 0),
    CONSTRAINT CK_Articles_EAN CHECK (EAN NOT LIKE '%[^0-9]%'),
    CONSTRAINT CK_Articles_Quantities CHECK (AvailableQuantity >= 0 AND MinQuantity >= 0 AND (MaxQuantity IS NULL OR MaxQuantity >= MinQuantity))
);

GO

CREATE UNIQUE INDEX UX_Articles_EAN ON Stock.Articles (EAN) WHERE EAN IS NOT NULL;
CREATE INDEX IX_Articles_BrandId ON Stock.Articles(BrandId);
CREATE INDEX IX_Articles_CategoryId ON Stock.Articles(CategoryId);

GO

CREATE TABLE Stock.BikeModels (
    Id INT NOT NULL IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    BrandId INT NOT NULL,
    Model NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    CategoryId INT NOT NULL,
    PurchasePriceExclVat DECIMAL(10, 2) NOT NULL,
    SalePriceInclVat DECIMAL(10, 2) NOT NULL,
    VatRateId INT NOT NULL,
    EAN NVARCHAR(13) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_BikeModels_IsActive DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_BikeModels_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_BikeModels_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_BikeModels PRIMARY KEY (Id),
    CONSTRAINT FK_BikeModels_Brands FOREIGN KEY (BrandId) REFERENCES Stock.Brands(Id),
    CONSTRAINT FK_BikeModels_Categories FOREIGN KEY (CategoryId) REFERENCES Stock.Categories(Id),
    CONSTRAINT FK_BikeModels_VatRates FOREIGN KEY (VatRateId) REFERENCES Stock.VatRates(Id),
    CONSTRAINT CK_BikeModels_EAN CHECK (EAN NOT LIKE '%[^0-9]%'),
    CONSTRAINT CK_BikeModels_Prices CHECK (PurchasePriceExclVat >= 0 AND SalePriceInclVat >= 0)
);

GO

CREATE UNIQUE INDEX UX_BikeModels_EAN ON Stock.BikeModels (EAN) WHERE EAN IS NOT NULL;
CREATE INDEX IX_BikeModels_BrandId ON Stock.BikeModels (BrandId);
CREATE INDEX IX_BikeModels_CategoryId ON Stock.BikeModels (CategoryId);

GO

CREATE TABLE Stock.BikeUnits (
    Id INT NOT NULL IDENTITY(1,1),
    BikeModelId INT NOT NULL,
    SerialNumber NVARCHAR(100) NOT NULL,
    LocationId INT NOT NULL,
    Status NVARCHAR(50) NOT NULL CONSTRAINT DF_BikeUnits_Status DEFAULT 'InStock',
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_BikeUnits_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_BikeUnits_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_BikeUnits PRIMARY KEY (Id),
    CONSTRAINT UQ_BikeUnits_SerialNumber UNIQUE (SerialNumber),
    CONSTRAINT FK_BikeUnits_BikeModels FOREIGN KEY (BikeModelId) REFERENCES Stock.BikeModels(Id),
    CONSTRAINT FK_BikeUnits_Locations FOREIGN KEY (LocationId) REFERENCES Stock.Locations(Id),
    CONSTRAINT CK_BikeUnits_Status CHECK (Status IN ('InStock', 'Reserved', 'Sold'))
);

GO

CREATE INDEX IX_BikeUnits_BikeModelId ON Stock.BikeUnits (BikeModelId);

GO

CREATE TABLE Stock.Images (
    Id INT NOT NULL IDENTITY(1,1),
    ArticleId INT NULL,
    BikeModelId INT NULL,
    Url NVARCHAR(255) NOT NULL,
    ImageName NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Images_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Images_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Images PRIMARY KEY (Id),
    CONSTRAINT FK_Images_Articles FOREIGN KEY (ArticleId) REFERENCES Stock.Articles(Id),
    CONSTRAINT FK_Images_BikeModels FOREIGN KEY (BikeModelId) REFERENCES Stock.BikeModels(Id),
    CONSTRAINT CK_Images_SingleOwner CHECK 
    ((ArticleId IS NOT NULL AND BikeModelId IS NULL)
     OR 
    (ArticleId IS NULL AND BikeModelId IS NOT NULL))
);

GO

CREATE INDEX IX_Images_ArticleId ON Stock.Images (ArticleId);
CREATE INDEX IX_Images_BikeModelId ON Stock.Images (BikeModelId);

GO

INSERT INTO Stock.VatRates (Label, Rate)
VALUES
('Standard', 21.00),
('Reduced', 6.00);

INSERT INTO Stock.Locations (Name)
VALUES
('Showroom'),
('Stock');

GO