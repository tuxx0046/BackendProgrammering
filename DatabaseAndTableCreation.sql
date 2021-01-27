--USE MASTER; DROP DATABASE IF EXISTS BigShop;
--CREATE DATABASE BigShop;

USE BigShop;
GO

---------------- Country Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Country;
CREATE TABLE Country
(
	Id INT NOT NULL IDENTITY(0,1),
	Name NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_Country_Id PRIMARY KEY (Id)
)
GO
--/////////////// Insert into Country Table ///////////////
INSERT INTO dbo.Country(Name)
VALUES
(N'Danmark'),
(N'Tyskland'),
(N'Frankrig'),
(N'Norge'),
(N'Rusland')
GO

SELECT * FROM dbo.Country


---------------- Zip Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Zip;
CREATE TABLE Zip
(
	Id INT NOT NULL IDENTITY(0,1),
	ZipCode VARCHAR(50) NOT NULL,
	City NVARCHAR(100) NOT NULL,
	Country_Id INT NOT NULL,
	CONSTRAINT PK_Zip_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Zip_Country FOREIGN KEY (Country_Id) REFERENCES Country(Id)
)
GO

--/////////////// Insert into Zip Table ///////////////
INSERT INTO dbo.Zip(Zip, City, Country_Id)
VALUES
('5000', 'Odense', 0), --Danmark
('10117', 'Berlin', 1), --Tyskland
('69007', 'Lyon', 2), --Frankrig
('0158', 'Oslo', 3), --Norge
('10100', 'Moskva', 4) --Rusland
GO

SELECT z.Id, z.Zip, z.City, c.Name as Country
FROM dbo.Zip z
LEFT JOIN dbo.Country c
ON z.Country_Id = c.Id

SELECT * FROM dbo.Zip

---------------- Warehouse Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Warehouse;
CREATE TABLE Warehouse
(
	Id INT NOT NULL IDENTITY(0,1),
	Name VARCHAR(50) NULL,
	Address NVARCHAR(100) NOT NULL,
	Zip_Id INT NOT NULL,
	CONSTRAINT PK_Warehouse_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Warehouse_Zip FOREIGN KEY (Zip_Id) REFERENCES Zip(Id)
)
GO

--/////////////// Insert into Warehouse Table ///////////////
INSERT INTO dbo.Warehouse(Name, Address, Zip_Id)
VALUES
('Varelager Odense', 'Centrumvej 13', 0), --Danmark
('Das Feine Haus', 'Grosse Strasse 58', 1), --Tyskland
('Viva La France', 'Calvados 27', 2), --Frankrig
('Varehus Norge', 'Snevei 8', 3), --Norge
(NULL, N'король Лев', 4) --Rusland
GO

SELECT * FROM dbo.Warehouse


---------------- Category Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Category;
CREATE TABLE Category
(
	Id INT NOT NULL IDENTITY(0,1),
	Name NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_Category_Id PRIMARY KEY (Id)
)

--/////////////// Insert into Category Table ///////////////
INSERT INTO dbo.Category(Name)
VALUES
('TV'), 
('Mobiltelefon'), 
('Tablet'), 
(N'Stationær PC'), 
('Laptop'), 
('Fryser'), 
('Kamera'), 
('Hukommelseskort'),
(N'Støvsuger')
GO

SELECT * FROM dbo.Category


---------------- Manufacturer Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Manufacturer;
CREATE TABLE Manufacturer
(
	Id INT NOT NULL IDENTITY(0,1),
	Name NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_Manufacturer_Id PRIMARY KEY (Id)
)

--/////////////// Insert into Manufacturer Table ///////////////
INSERT INTO dbo.Manufacturer(Name)
VALUES
('Philips'), --TV
('Sony'), -- Mobiltelefon
('Samsung'), --Tablet
('Dell'), --Stationær PC
('Lenovo'), --Laptop
('Electrolux'), --Fryser
('Nikon'), --Kamera
('Sandberg'),--Hukommelseskort
('Dyson') --Støvsuger
GO

SELECT * FROM dbo.Manufacturer;
SELECT * FROM dbo.Category


---------------- Product Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Product;
CREATE TABLE Product
(
	Id INT NOT NULL IDENTITY(0,1),
	Name NVARCHAR(150) NOT NULL,
	Price DECIMAL(7,2) NOT NULL,
	EAN VARCHAR(20) NULL,
	WeightGram INT NOT NULL,
	Manufacturer_Id INT NOT NULL,
	Category_Id INT NOT NULL,
	CONSTRAINT PK_Product_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Product_Manufacturer FOREIGN KEY (Manufacturer_Id) REFERENCES Manufacturer(Id),
	CONSTRAINT FK_Product_Category FOREIGN KEY (Category_Id) REFERENCES Category(Id)
)

--/////////////// Insert into Product Table ///////////////
INSERT INTO dbo.Product(Name, Price, EAN, WeightGram, Manufacturer_Id, Category_Id)
VALUES
('Philips 28" TV', 4499.99, '2564897896542', 4500, 0, 0), --TV
('Sony XPhone 1', 2500.00, '1246985364258', 549, 1, 1), -- Mobiltelefon
('Samsung Galaxy Phone', 6999.98, '4536985423416', 677, 2, 1), -- Mobiltelefon
('Samsung Galaxy Tablet', 8666.49, '2364587953125', 855, 2, 2), --Tablet
('Dell Inspiron Desktop', 4499.00, '5489756321453', 2335, 3, 3), --Stationær PC
('Lenovo Yoga Laptop', 6899.99, '2546987563214', 1307, 4, 4), --Laptop
('Electrolux Isskab', 4559.00, '5698745632145', 13400, 5, 5), --Fryser
('Nikon FF DSLR', 12000.00, '5698756321456', 607, 6, 6), --Kamera
('Sandberg 64GB Micro SD Card', 119.99, '1321412365891', 2, 7, 7),--Hukommelseskort
('Dyson Ball Animal', 654.99, '5421365248971', 2766, 8, 8), --Støvsuger
(N'Philips Støvsuger', 300.00, '2134568795632', 1520, 0, 8), --Støvsuger
('Sony 128GB SD Card', 239.50, '5469821354631', 2, 1, 7),--Hukommelseskort
('Sony A6600', 8669.99, '1254681563214', 517, 1, 6), --Kamera
('Philips Frys-a-lot', 2999.00, '2135468941234', 10200, 0, 5), --Fryser
('Sony VAIO', 5999.99, '5412365478123', 2077, 1, 4), --Laptop
('Dell XPS Laptop', 13799.00, '2135468311456', 2708, 3, 4), --Laptop
('Electrolux Hjemmelavet PC', 7499.00, '2456887411312', 4055, 5, 3), --Stationær PC
('Samsung 28" TV', 6000.00, '8956478596542', 2200, 2, 0) --TV
GO

SELECT * FROM dbo.Product
SELECT * FROM dbo.Manufacturer;
SELECT * FROM dbo.Category


---------------- Warehouse_Product Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Warehouse_Product;
CREATE TABLE Warehouse_Product
(
	Quantity INT NOT NULL,
	Product_Id INT NOT NULL,
	Warehouse_Id INT NOT NULL,
	CONSTRAINT FK_Warehouse_Product_Product FOREIGN KEY (Product_Id) REFERENCES Product(Id),
	CONSTRAINT FK_Warehouse_Product_Warehouse FOREIGN KEY (Warehouse_Id) REFERENCES Warehouse(Id)
)


--/////////////// Insert into Warehouse_Product Table ///////////////
INSERT INTO dbo.Warehouse_Product(Quantity, Product_Id, Warehouse_Id)
VALUES
------- Danmark Odense
(150, 0, 0), --Philips TV, Odense Danmark
(300, 1, 0), --Sony mobil, Odense Danmark
(750, 2, 0), --Samsung mobil, Odense Danmark
(100, 3, 0), --Samsung tablet, Odense Danmark
(25, 4, 0), --Dell Desktop, Odense Danmark
(35, 5, 0), --Lenovo laptop, Odense Danmark
(24, 6, 0), --Electrolux fryser, Odense Danmark
(230, 7, 0), --Nikon kamera, Odense Danmark
(1000, 8, 0), --Sandberg hukommelseskort, Odense Danmark
(60, 9, 0), --Dyson støvsuger, Odense Danmark
(100, 10, 0), --Philips støvsuger, Odense Danmark
(400, 11, 0), --Sony hukommelseskort, Odense Danmark
(100, 12, 0), --Sony kamera, Odense Danmark
(66, 13, 0), --Philips fryser, Odense Danmark
(21, 14, 0), --Sony laptop, Odense Danmark
(300, 15, 0), --Dell laptop, Odense Danmark
(20, 16, 0), --Electrolux PC, Odense Danmark
(94, 17, 0), --Samsung TV, Odense Danmark
------- Tyskland Berlin
(75, 0, 1), --Philips TV, Berlin Tyskland
(121, 1, 1), --Sony mobil, Berlin Tyskland
(642, 2, 1), --Samsung mobil, Berlin Tyskland
(663, 3, 1), --Samsung tablet, Berlin Tyskland
(231, 4, 1), --Dell Desktop, Berlin Tyskland
(454, 5, 1), --Lenovo laptop, Berlin Tyskland
(33, 6, 1), --Electrolux fryser, Berlin Tyskland
(61, 7, 1), --Nikon kamera, Berlin Tyskland
(998, 8, 1), --Sandberg hukommelseskort, Berlin Tyskland
(230, 9, 1), --Dyson støvsuger, Berlin Tyskland
(654, 10, 1), --Philips støvsuger, Berlin Tyskland
(123, 11, 1), --Sony hukommelseskort, Berlin Tyskland
(111, 12, 1), --Sony kamera, Berlin Tyskland
(75, 13, 1), --Philips fryser, Berlin Tyskland
(33, 14, 1), --Sony laptop, Berlin Tyskland
(361, 15, 1), --Dell laptop, Berlin Tyskland
(55, 16, 1), --Electrolux PC, Berlin Tyskland
(120, 17, 1), --Samsung TV, Berlin Tyskland
------- Frankrig Lyon
(12, 0, 2), --Philips TV, Frankrig Lyon
(445, 1, 2), --Sony mobil, Frankrig Lyon
(135, 2, 2), --Samsung mobil, Frankrig Lyon
(22, 3, 2), --Samsung tablet, Frankrig Lyon
(156, 4, 2), --Dell Desktop, Frankrig Lyon
(89, 5, 2), --Lenovo laptop, Frankrig Lyon
(789, 6, 2), --Electrolux fryser, Frankrig Lyon
(129, 7, 2), --Nikon kamera, Frankrig Lyon
(5233, 8, 2), --Sandberg hukommelseskort, Frankrig Lyon
(4123, 9, 2), --Dyson støvsuger, Frankrig Lyon
(12, 10, 2), --Philips støvsuger, Frankrig Lyon
(335, 11, 2), --Sony hukommelseskort, Frankrig Lyon
(657, 12, 2), --Sony kamera, Frankrig Lyon
(100, 13, 2), --Philips fryser, Frankrig Lyon
(549, 14, 2), --Sony laptop, Frankrig Lyon
(227, 15, 2), --Dell laptop, Frankrig Lyon
(469, 16, 2), --Electrolux PC, Frankrig Lyon
(236, 17, 2), --Samsung TV, Frankrig Lyon
------- Norge Oslo
(879, 0, 3), --Philips TV, Norge Oslo
(86, 1, 3), --Sony mobil, Norge Oslo
(123, 2, 3), --Samsung mobil, Norge Oslo
(668, 3, 3), --Samsung tablet, Norge Oslo
(453, 4, 3), --Dell Desktop, Norge Oslo
(97, 5, 3), --Lenovo laptop, Norge Oslo
(566, 6, 3), --Electrolux fryser, Norge Oslo
(324, 7, 3), --Nikon kamera, Norge Oslo
(21, 8, 3), --Sandberg hukommelseskort, Norge Oslo
(852, 9, 3), --Dyson støvsuger, Norge Oslo
(65, 10, 3), --Philips støvsuger, Norge Oslo
(1, 11, 3), --Sony hukommelseskort, Norge Oslo
(335, 12, 3), --Sony kamera, Norge Oslo
(698, 13, 3), --Philips fryser, Norge Oslo
(447, 14, 3), --Sony laptop, Norge Oslo
(361, 15, 3), --Dell laptop, Norge Oslo
(99, 16, 3), --Electrolux PC, Norge Oslo
(87, 17, 3), --Samsung TV, Norge Oslo
------- Rusland Moskva
(555, 0, 4), --Philips TV, Rusland Moskva
(569, 1, 4), --Sony mobil, Rusland Moskva
(223, 2, 4), --Samsung mobil, Rusland Moskva
(78, 3, 4), --Samsung tablet, Rusland Moskva
(23, 4, 4), --Dell Desktop, Rusland Moskva
(256, 5, 4), --Lenovo laptop, Rusland Moskva
(46, 6, 4), --Electrolux fryser, Rusland Moskva
(92, 7, 4), --Nikon kamera, Rusland Moskva
(662, 8, 4), --Sandberg hukommelseskort, Rusland Moskva
(12, 9, 4), --Dyson støvsuger, Rusland Moskva
(789, 10, 4), --Philips støvsuger, Rusland Moskva
(233, 11, 4), --Sony hukommelseskort, Rusland Moskva
(3, 12, 4), --Sony kamera, Rusland Moskva
(613, 13, 4), --Philips fryser, Rusland Moskva
(77, 14, 4), --Sony laptop, Rusland Moskva
(13, 15, 4), --Dell laptop, Rusland Moskva
(556, 16, 4), --Electrolux PC, Rusland Moskva
(23, 17, 4) --Samsung TV, Rusland Moskva
GO

SELECT wp.Quantity, p.Name as 'Product', w.Name as 'Warehouse name', c.Name as 'Category', cn.Name as 'Country'
FROM Warehouse_Product wp
LEFT JOIN Product p
ON wp.Product_Id = p.Id
LEFT JOIN Warehouse w
ON wp.Warehouse_Id = w.Id
LEFT JOIN Category c
ON p.Category_Id = c.Id
LEFT JOIN Zip z
ON w.Zip_Id = z.Id
LEFT JOIN Country cn
ON z.Country_Id = cn.Id
ORDER BY p.Id


SELECT z.Id, z.Zip, z.City, c.Name as Country
FROM dbo.Zip z
LEFT JOIN dbo.Country c
ON z.Country_Id = c.Id

---------------- Customer Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Customer;
CREATE TABLE Customer
(
	Id INT NOT NULL IDENTITY(0,1),
	FirstName VARCHAR(50) NULL,
	LastName VARCHAR(100) NULL,
	Email VARCHAR(70) NULL,
	Address NVARCHAR(100) NOT NULL,
	Phone VARCHAR(50) NOT NULL,
	Zip_Id INT NOT NULL,
	Country_Id INT NOT NULL,
	CONSTRAINT PK_Customer_Id PRIMARY KEY (Id),
	CONSTRAINT UNIQUE_Customer_Email UNIQUE(Email),
	CONSTRAINT FK_Customer_Zip FOREIGN KEY (Zip_Id) REFERENCES Zip(Id),
	CONSTRAINT FK_Customer_Country FOREIGN KEY (Country_Id) REFERENCES Country(Id)
)

---------------- PaymentMethod Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.PaymentMethod;
CREATE TABLE PaymentMethod
(
	Id INT NOT NULL IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL,
	CONSTRAINT PK_PaymentMethod_Id PRIMARY KEY (Id)
)

---------------- Courier Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Courier;
CREATE TABLE Courier
(
	Id INT NOT NULL IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL,
	InitialFee DECIMAL(7,2) NOT NULL,
	WeightFee DECIMAL(7,2) NOT NULL,
	CONSTRAINT PK_Courier_Id PRIMARY KEY (Id)
)

---------------- CustomerOrder Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.CustomerOrder;
CREATE TABLE CustomerOrder
(
	Id BIGINT NOT NULL IDENTITY(0,1),
	OrderDate DATETIME2 NOT NULL DEFAULT GETDATE(),
	ShippingFee DECIMAL(7,2) NOT NULL,
	Customer_Id INT NOT NULL,
	Courier_Id INT NOT NULL,
	PaymentMethod_Id INT NOT NULL,
	CONSTRAINT PK_Order_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Order_Customer FOREIGN KEY (Customer_Id) REFERENCES Customer(Id),
	CONSTRAINT FK_Order_Courier FOREIGN KEY (Courier_Id) REFERENCES Courier(Id),
	CONSTRAINT FK_Order_PaymentMethod FOREIGN KEY (PaymentMethod_Id) REFERENCES PaymentMethod(Id)
)

---------------- OrderLine Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.OrderLine;
CREATE TABLE OrderLine
(
	Quantity INT NOT NULL,
	Price DECIMAL(7,2) NOT NULL,
	Product_Id INT NOT NULL,
	CustomerOrder_Id BIGINT NOT NULL,
	CONSTRAINT FK_OrderLine_Product FOREIGN KEY (Product_Id) REFERENCES Product(Id),
	CONSTRAINT FK_OrderLine_CustomerOrder FOREIGN KEY (CustomerOrder_Id) REFERENCES CustomerOrder(Id)
)

---------------- Position Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Position;
CREATE TABLE Position
(
	Id INT NOT NULL IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL,
	CONSTRAINT PK_Position_Id PRIMARY KEY (Id)
)

---------------- Department Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Department;
CREATE TABLE Department
(
	Id INT NOT NULL IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL,
	Phone VARCHAR(50) NULL,
	Warehouse_Id INT NOT NULL,
	CONSTRAINT PK_Department_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Department_Warehouse FOREIGN KEY (Warehouse_Id) REFERENCES Warehouse(Id)
)

---------------- Employee Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Employee;
CREATE TABLE Employee
(
	Id INT NOT NULL IDENTITY(0,1),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	Phone VARCHAR(50) NULL,
	Position_Id INT NOT NULL,
	Department_Id INT NOT NULL,
	CONSTRAINT PK_Employee_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Employee_Position FOREIGN KEY (Position_Id) REFERENCES Position(Id),
	CONSTRAINT FK_Employee_Deparment FOREIGN KEY (Department_Id) REFERENCES Department(Id)
)

---------------- OrderStatus Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.OrderStatus;
CREATE TABLE OrderStatus
(
	Id INT NOT NULL IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL,
	CONSTRAINT PK_OrderStatus_Id PRIMARY KEY (Id)
)

---------------- OrderProcess Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.OrderProcess;
CREATE TABLE OrderProcess
(
	ProcessDate DATETIME2 NOT NULL DEFAULT GETDATE(),
	Employee_Id INT NOT NULL,
	CustomerOrder_Id BIGINT NOT NULL,
	OrderStatus_Id INT NOT NULL,
	CONSTRAINT FK_OrderProcess_Employee FOREIGN KEY (Employee_Id) REFERENCES Employee(Id),
	CONSTRAINT FK_OrderProcess_CustomerOrder FOREIGN KEY (CustomerOrder_Id) REFERENCES CustomerOrder(Id),
	CONSTRAINT FK_OrderProcess_OrderStatus FOREIGN KEY (OrderStatus_Id) REFERENCES OrderStatus(Id)
)