USE BikeShopManagerWebApp;
GO

CREATE SCHEMA Shop;

GO

CREATE TABLE Shop.Orders (
    Id INT NOT NULL IDENTITY(1,1),
    CustomerId INT NOT NULL,
    EmployeeId INT NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    QuoteNumber NVARCHAR(50) NOT NULL,
    CommandNumber NVARCHAR(50) NULL,
    QuoteValidUntil DATETIME2 NULL,
    ConfirmedAt DATETIME2 NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Orders_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Orders_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Orders PRIMARY KEY (Id),
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Persons.Customers(Id),
    CONSTRAINT FK_Orders_Employees FOREIGN KEY (EmployeeId) REFERENCES Persons.Employees(Id),
    CONSTRAINT UQ_Orders_QuoteNumber UNIQUE (QuoteNumber)
);

GO

CREATE TABLE Shop.OrderLines (
    Id INT NOT NULL IDENTITY(1,1),
    OrderId INT NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    ArticleId INT NULL,
    BikeModelId INT NULL,
    BikeUnitId INT NULL,
    Label NVARCHAR(50) NULL,
    Quantity INT NOT NULL,
    UnitPriceExclVat DECIMAl(12,2) NOT NULL,
    VatRateId INT NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_OrderLines_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_OrderLines_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_OrderLines PRIMARY KEY (Id),
    CONSTRAINT FK_OrderLines_Orders FOREIGN KEY (OrderId) REFERENCES Shop.Orders(Id),
    CONSTRAINT FK_OrderLines_Articles FOREIGN KEY (ArticleId) REFERENCES Stock.Articles(Id),
    CONSTRAINT FK_OrderLines_BikeModels FOREIGN KEY (BikeModelId) REFERENCES Stock.BikeModels(Id),
    CONSTRAINT FK_OrderLines_BikeUnits FOREIGN KEY (BikeUnitId) REFERENCES Stock.BikeUnits(Id),
    CONSTRAINT CH_OrderLines_Quantity CHECK (Quantity > 0)
);

GO

CREATE TABLE Shop.DeliveryNotes (
    Id INT NOT NULL IDENTITY(1,1),
    OrderId INT NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    CustomerNotifiedDate DATETIME2 NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_DeliveryNotes_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_DeliveryNotes_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_DeliveryNotes PRIMARY KEY (Id),
    CONSTRAINT UQ_DeliveryNotes_OrderId UNIQUE (OrderId),
    CONSTRAINT FK_DeliveryNotes_Orders FOREIGN KEY (OrderId) REFERENCES Shop.Orders(Id)
);

GO

CREATE TABLE Shop.Sales (
    Id INT NOT NULL IDENTITY(1,1),
    CustomerId INT NULL,
    EmployeeId INT NOT NULL,
    DeliveryNoteId INT NULL,
    Status NVARCHAR(50) NOT NULL,
    TotalExclVat DECIMAL(12,2) NOT NULL,
    TotalVat DECIMAL(12,2) NOT NULL,
    TotalInclVat DECIMAL(12,2) NOT NULL,
    SaleDate DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Sales_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Sales_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Sales PRIMARY KEY (Id),
    CONSTRAINT FK_Sales_Customers FOREIGN KEY (CustomerId) REFERENCES Persons.Customers(Id),
    CONSTRAINT FK_Sales_Employees FOREIGN KEY (EmployeeId) REFERENCES Persons.Employees(Id),
    CONSTRAINT FK_Sales_DeliveryNotes FOREIGN KEY (DeliveryNoteId) REFERENCES Shop.DeliveryNotes(Id)
    --Check on Status
);

GO

CREATE TABLE Shop.SaleLines (
    Id INT NOT NULL IDENTITY(1,1),
    SaleId INT NOT NULL,
    Label NVARCHAR(50) NOT NULL,
    Quantity INT NOT NULL CHECK(Quantity > 0),
    UnitPriceExclVat DECIMAL(12,2) NOT NULL,
    VatRate DECIMAL(5,2) NOT NULL,
    ArticleId INT NULL,
    BikeUnitId INT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_SaleLines_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_SaleLines_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_SaleLines PRIMARY KEY (Id),
);

GO

CREATE TABLE Shop.Invoices (
    Id INT NOT NULL IDENTITY(1,1),
    InvoiceNumber NVARCHAR(20) UNIQUE NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    IssuedDate DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Invoices_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Invoices_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Invoices PRIMARY KEY (Id),
);

GO
CREATE TABLE Shop.Payments (
    Id INT NOT NULL IDENTITY(1,1),
    SaleId INT NULL,
    OrderId INT NULL,
    Amount DECIMAL(12,2) NOT NULL CHECK(Amount > 0),
    PaymentMethod NVARCHAR(20) NOT NULL,
    PaymentDate DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Payments_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Payments_UpdatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Payments PRIMARY KEY (Id),
);

GO

CREATE TABLE Shop.AuditLogs (
    Id INT NOT NULL IDENTITY(1,1),
    EmployeeId INT NULL,
    Action NVARCHAR(50) NOT NULL,
    EntityName NVARCHAR(50) NOT NULL,
    EntityId INT NULL,
    Details NVARCHAR(MAX) NULL,
    IpAddress NVARCHAR(45) NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_AuditLogs_CreatedAt DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_AuditLogs PRIMARY KEY (Id),
);

GO