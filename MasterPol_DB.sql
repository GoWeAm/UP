-- Партнеры
CREATE TABLE Partners (
    PartnerID INT IDENTITY(1,1) PRIMARY KEY,
    PartnerType NVARCHAR(50) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    LegalAddress NVARCHAR(500),
    INN NVARCHAR(20),
    DirectorFullName NVARCHAR(200),
    Phone NVARCHAR(50),
    Email NVARCHAR(200),
    Rating DECIMAL(3,2) DEFAULT 0
);

-- Менеджеры
CREATE TABLE Managers (
    ManagerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(50),
    Email NVARCHAR(200)
);

-- Поставщики
CREATE TABLE Suppliers (
    SupplierID INT IDENTITY(1,1) PRIMARY KEY,
    SupplierType NVARCHAR(100),
    Name NVARCHAR(200) NOT NULL,
    INN NVARCHAR(20)
);

-- Материалы
CREATE TABLE Materials (
    MaterialID INT IDENTITY(1,1) PRIMARY KEY,
    MaterialType NVARCHAR(100),
    Name NVARCHAR(200) NOT NULL,
    SupplierID INT FOREIGN KEY REFERENCES Suppliers(SupplierID),
    QuantityPerPackage DECIMAL(10,2),
    Unit NVARCHAR(20),
    CostPerUnit DECIMAL(10,2),
    QuantityOnStock DECIMAL(10,2)
);

-- Продукция
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    SKU NVARCHAR(50) NOT NULL UNIQUE,
    ProductType NVARCHAR(100),
    Name NVARCHAR(200) NOT NULL,
    MinPriceForPartner DECIMAL(10,2)
);

-- Заявки
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    PartnerID INT FOREIGN KEY REFERENCES Partners(PartnerID),
    ManagerID INT FOREIGN KEY REFERENCES Managers(ManagerID),
    OrderDate DATE,
    Status NVARCHAR(50) DEFAULT 'Created',
    PrepaymentAmount DECIMAL(10,2),
    TotalAmount DECIMAL(10,2)
);

-- Позиции заявок
CREATE TABLE OrderItems (
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT,
    Price DECIMAL(10,2)
);

-- История продаж партнеров
CREATE TABLE PartnerSalesHistory (
    SaleID INT IDENTITY(1,1) PRIMARY KEY,
    PartnerID INT FOREIGN KEY REFERENCES Partners(PartnerID),
    SaleDate DATE,
    Amount DECIMAL(12,2)
);
-- Роли пользователей
CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200)
);

-- Пользователи
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Email NVARCHAR(200),
    RoleID INT FOREIGN KEY REFERENCES Roles(RoleID),
    ManagerID INT NULL FOREIGN KEY REFERENCES Managers(ManagerID),
    PartnerID INT NULL FOREIGN KEY REFERENCES Partners(PartnerID),
    CreatedAt DATETIME DEFAULT GETDATE(),
    LastLogin DATETIME
);
