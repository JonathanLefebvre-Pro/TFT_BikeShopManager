USE TFT_BikeShopManager_WebApi;

GO

--Delete Persons schema and all its tables
DROP TABLE IF EXISTS Persons.RefreshTokens;
DROP TABLE IF EXISTS Persons.EmployeeRoles;
DROP TABLE IF EXISTS Persons.Customers;
DROP TABLE IF EXISTS Persons.Employees;
DROP TABLE IF EXISTS Persons.Roles;
DROP TABLE IF EXISTS Persons.Users;
DROP SCHEMA IF EXISTS Persons;

GO

CREATE SCHEMA Persons;

GO

CREATE TABLE Persons.Users (
    Id INT NOT NULL IDENTITY(1,1),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(256) NULL,
    PhoneNumber NVARCHAR(20) NULL,
    Address NVARCHAR(200) NULL,
    City NVARCHAR(50) NULL,
    ZipCode NVARCHAR(10) NULL,
    Country NVARCHAR(50) NULL,
    BirthDate DATE NULL,
    AllowMarketing BIT NOT NULL CONSTRAINT DF_Users_AllowMarketing DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Users_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Users_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Users PRIMARY KEY (Id)
);

GO

CREATE UNIQUE INDEX UX_Users_Email ON Persons.Users (Email) WHERE Email IS NOT NULL;
CREATE INDEX IX_Users_LastName_FirstName ON Persons.Users (LastName, FirstName);

GO

CREATE TABLE Persons.Customers (
    Id INT NOT NULL IDENTITY(1,1),
    UserId INT NOT NULL,
    VatNumber NVARCHAR(20) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Customers_IsActive DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Customers_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Customers_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Customers PRIMARY KEY (Id),
    CONSTRAINT UQ_Customers_UserId UNIQUE (UserId),
    CONSTRAINT FK_Customers_Users FOREIGN KEY (UserId) REFERENCES Persons.Users(Id)
);

GO

CREATE TABLE Persons.Roles (
    Id INT NOT NULL IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    NormalizedName NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Roles_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Roles_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Roles PRIMARY KEY (Id),
    CONSTRAINT UQ_Roles_Name UNIQUE (Name),
    CONSTRAINT UQ_Roles_NormalizedName UNIQUE (NormalizedName)
);

GO

CREATE TABLE Persons.Employees (
    Id INT NOT NULL IDENTITY(1,1),
    UserId INT NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    SecurityStamp NVARCHAR(64) NOT NULL,
    AccessFailedCount INT NOT NULL CONSTRAINT DF_Employees_AccessFailedCount DEFAULT 0,
    LockoutEnd DATETIMEOFFSET NULL,
    HiredAt DATE NOT NULL,
    ContractEndAt DATE NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Employees_IsActive DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Employees_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Employees_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Employees PRIMARY KEY (Id),
    CONSTRAINT UQ_Employees_UserId UNIQUE (UserId),
    CONSTRAINT FK_Employees_Users FOREIGN KEY (UserId) REFERENCES Persons.Users(Id),
    CONSTRAINT CK_Employees_Contract CHECK (ContractEndAt IS NULL OR ContractEndAt >= HiredAt)
);

GO

CREATE TABLE Persons.EmployeeRoles (
    EmployeeId INT NOT NULL,
    RoleId INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_EmployeeRoles_CreatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_EmployeeRoles PRIMARY KEY (EmployeeId, RoleId),
    CONSTRAINT FK_EmployeeRoles_Employees FOREIGN KEY (EmployeeId) REFERENCES Persons.Employees(Id),
    CONSTRAINT FK_EmployeeRoles_Roles FOREIGN KEY (RoleId) REFERENCES Persons.Roles(Id)
);

GO

CREATE INDEX IX_EmployeeRoles_RoleId ON Persons.EmployeeRoles (RoleId);

GO

CREATE TABLE Persons.RefreshTokens (
    Id INT NOT NULL IDENTITY(1,1),
    EmployeeId INT NOT NULL,
    TokenHash NVARCHAR(255) NOT NULL,
    ExpiresAt DATETIME2 NOT NULL,
    RevokedAt DATETIME2 NULL,
    ReplacedByTokenHash NVARCHAR(255) NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_RefreshTokens_CreatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_RefreshTokens PRIMARY KEY (Id),
    CONSTRAINT UQ_RefreshTokens_TokenHash UNIQUE (TokenHash),
    CONSTRAINT FK_RefreshTokens_Employees FOREIGN KEY (EmployeeId) REFERENCES Persons.Employees(Id) ON DELETE CASCADE
);
GO

CREATE INDEX IX_RefreshTokens_EmployeeId ON Persons.RefreshTokens (EmployeeId);

GO

INSERT INTO Persons.Roles (Name, NormalizedName)
VALUES 
('Admin', 'ADMIN'), 
('Manager', 'MANAGER'), 
('Seller', 'SELLER'), 
('Mechanic', 'MECHANIC');

GO

USE TFT_BikeShopManager_WebApi;
SELECT * FROM Persons.Users
SELECT * FROM Persons.Customers
SELECT * FROM Persons.Employees
SELECT * FROM Persons.Roles
SELECT * FROM Persons.EmployeeRoles

GO