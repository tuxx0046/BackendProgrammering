  --------Category
  SELECT *
  FROM [BigShopDB].[dbo].[Category]
   
  --------Product
  SELECT *
  FROM [BigShopDB].[dbo].[Product]

  --------Manufacturer
  SELECT *
  FROM [BigShopDB].[dbo].[Manufacturer]

  --------ApplicationUser
  SELECT *
  FROM [BigShopDB].[dbo].[ApplicationUser]





--USE MASTER; DROP DATABASE IF EXISTS BigShopdb;
CREATE DATABASE BigShop;

USE BigShop;
GO

---------------- Country Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Country;
CREATE TABLE Country
(
	Id INT NOT NULL IDENTITY(0,1),
	[Name] NVARCHAR(100) NOT NULL,
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
	CityName NVARCHAR(100) NOT NULL,
	Country_Id INT NOT NULL,
	CONSTRAINT PK_Zip_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Zip_Country FOREIGN KEY (Country_Id) REFERENCES dbo.Country(Id)
)
GO

--/////////////// Insert into Zip Table ///////////////
INSERT INTO dbo.Zip(ZipCode, CityName, Country_Id)
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
	AddressLane NVARCHAR(100) NOT NULL,
	Zip_Id INT NOT NULL,
	CONSTRAINT PK_Warehouse_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Warehouse_Zip FOREIGN KEY (Zip_Id) REFERENCES Zip(Id)
)
GO

--/////////////// Insert into Warehouse Table ///////////////
INSERT INTO dbo.Warehouse(Name, AddressLane, Zip_Id)
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
	EAN VARCHAR(13) NULL,
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
	Quantity INT NOT NULL DEFAULT 0,
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
ORDER BY p.Id ASC, cn.Name ASC

---------------- ApplicationUser Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.applicationuser;
CREATE TABLE ApplicationUser
(
	Id INT NOT NULL IDENTITY(0,1),
	Username VARCHAR(20) NULL,
	NormalizedUsername VARCHAR(20) NULL,
	PasswordHash NVARCHAR(MAX) NULL,
	CONSTRAINT PK_ApplicationUser_Id PRIMARY KEY (Id)
)

--/////////////// Insert into ApplicationUser Table ///////////////
INSERT INTO dbo.ApplicationUser(Username, NormalizedUsername, PasswordHash)
VALUES
('CUSTOMER','customer', 'Admin123'), --Customer
('EMPLOYEE','employee', 'Admin123') --Employee



---------------- Customer Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Customer;
CREATE TABLE Customer
(
	Id INT NOT NULL IDENTITY(0,1),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	Email VARCHAR(70) NOT NULL,
	NormalizedEmail VARCHAR(70) NULL,
	AddressLane NVARCHAR(100) NOT NULL,
	Phone VARCHAR(50) NOT NULL,
	Zip_Id INT NOT NULL,
	ApplicationUser_Id INT NOT NULL,
	CONSTRAINT PK_Customer_Id PRIMARY KEY (Id),
	CONSTRAINT UNIQUE_Customer_NormalizedEmail UNIQUE(NormalizedEmail),
	CONSTRAINT FK_Customer_Zip FOREIGN KEY (Zip_Id) REFERENCES Zip(Id),
	CONSTRAINT FK_Customer_ApplicationUser FOREIGN KEY (ApplicationUser_Id) REFERENCES ApplicationUser(Id)
)
GO

--/////////////// Insert into Customer Table ///////////////
INSERT INTO dbo.Customer(FirstName, LastName, Email, NormalizedEmail, AddressLane, Phone, Zip_Id, ApplicationUser_Id)
VALUES
(N'Jørgen', N'Ådahl', 'JAA@karma.dk', 'jaa@karma.dk', 'Strandvej 115', '65487566', 0, 0), --5000, Danmark
(N'Miauw', N'Katzemann', 'NYAAA@cat.de', 'nyaaa@cat.de', 'Eine kleine katzmuzik 44', '55213687', 1, 0), --10117, Tyskland
(N'Michel', N'Foucault', 'discourse@lafrance.fr', 'discourse@lafrance.fr', 'Archeology of Knowlede boulevard 32', '41023698', 2, 0), --69007, Frankrig
(N'Bamse', N'Kylling', 'bamseogkylling@yesplz.no', 'bamseogkylling@yesplz.no', 'Oslostraede 87', '54879632', 3, 0), --0158, Norge
(N'Karl', N'Marx', 'marx@comm.com', 'marx@comm.com', 'Vodka lane 1', '55648975' , 4, 0) --10100, Rusland
GO

SELECT * FROM Customer

SELECT z.Id, z.ZipCode, c.Name as Country
FROM ZIP z
LEFT JOIN Country c
ON z.Country_Id = c.Id


---------------- PaymentMethod Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.PaymentMethod;
CREATE TABLE PaymentMethod
(
	Id INT NOT NULL IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL,
	CONSTRAINT PK_PaymentMethod_Id PRIMARY KEY (Id)
)

--/////////////// Insert into PaymentMethod Table ///////////////
INSERT INTO dbo.PaymentMethod(Name)
VALUES
('VISA'),
('Mastercard'),
('Dankort'),
('American Express'),
('Paypal'),
('Bank transfer')
GO

SELECT * FROM dbo.PaymentMethod

---------------- Courier Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Courier;
CREATE TABLE Courier
(
	Id INT NOT NULL IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL,
	InitialCost DECIMAL(7,2) NOT NULL,
	WeightFee DECIMAL(7,2) NOT NULL,
	CONSTRAINT PK_Courier_Id PRIMARY KEY (Id)
)

--/////////////// Insert into Courier Table ///////////////
INSERT INTO dbo.Courier(Name, InitialCost, WeightFee)
VALUES
('DHL', 79.00, 22.23),
('GLS', 55.95, 25.00),
('PostNord', 89.00, 23.11),
('DeliverXpert', 123.00, 16.76)

SELECT * FROM dbo.Courier


---------------- CustomerOrder Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.CustomerOrder;
CREATE TABLE CustomerOrder
(
	Id BIGINT NOT NULL IDENTITY(0,1),
	OrderDate DATETIME2 NOT NULL DEFAULT GETDATE(),
	InitialShippingCost DECIMAL(7,2) NOT NULL,
	WeightFee DECIMAL(7,2) NOT NULL,
	Customer_Id INT NOT NULL,
	Courier_Id INT NOT NULL,
	PaymentMethod_Id INT NOT NULL,
	CONSTRAINT PK_Order_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Order_Customer FOREIGN KEY (Customer_Id) REFERENCES Customer(Id),
	CONSTRAINT FK_Order_Courier FOREIGN KEY (Courier_Id) REFERENCES Courier(Id),
	CONSTRAINT FK_Order_PaymentMethod FOREIGN KEY (PaymentMethod_Id) REFERENCES PaymentMethod(Id)
)

--/////////////// Insert into Customer Table ///////////////
INSERT INTO dbo.CustomerOrder(OrderDate, InitialShippingCost, WeightFee, Customer_Id, Courier_Id, PaymentMethod_Id)
VALUES
('2020-02-21 11:42:53', (SELECT InitialCost FROM dbo.Courier WHERE Id = 0), (SELECT WeightFee FROM dbo.Courier WHERE Id = 0), 0, 0, 0), --Jørgen, DHL, VISA
('2020-03-19 08:11:13',(SELECT InitialCost FROM dbo.Courier WHERE Id = 1), (SELECT WeightFee FROM dbo.Courier WHERE Id = 1), 1, 1, 1), --Miauw, GLS, Mastercard
('2020-07-03 10:26:23', (SELECT InitialCost FROM dbo.Courier WHERE Id = 2), (SELECT WeightFee FROM dbo.Courier WHERE Id = 2), 2, 2, 2), --Michel, PostNord, Dankort
('2020-08-25 15:01:03', (SELECT InitialCost FROM dbo.Courier WHERE Id = 3), (SELECT WeightFee FROM dbo.Courier WHERE Id = 3), 3, 3, 3), --Bamse, DeliverXpert, American Express
('2021-01-29 10:22:38', (SELECT InitialCost FROM dbo.Courier WHERE Id = 0), (SELECT WeightFee FROM dbo.Courier WHERE Id = 0), 4, 0, 4) --Karl, DHL, Paypal


SELECT co.Id, CONVERT(date, co.OrderDate) as 'Order date', co.InitialShippingCost, co.WeightFee, c.Id as CustomerId, CONCAT_WS(' ', c.FirstName, c.LastName) as Customer, crr.Name as Courier, p.Name as 'Payment Method' 
FROM dbo.CustomerOrder co
LEFT JOIN Customer c
ON co.Customer_Id = c.Id
LEFT JOIN Courier crr
ON co.Courier_Id = crr.Id
LEFT JOIN PaymentMethod p
ON co.PaymentMethod_Id = p.Id
ORDER BY 'Order date'

SELECT * FROM CustomerOrder
SELECT GETDATE()
SELECT * FROM Customer
SELECT * FROM Courier
SELECT * FROM PaymentMethod

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

--/////////////// Insert into OrderLine Table ///////////////
INSERT INTO dbo.OrderLine(Quantity, Price, Product_Id, CustomerOrder_Id)
VALUES
(1, (SELECT Price FROM dbo.Product WHERE Id = 0), 0, 0), -- Philips TV, Jørgen
(2, (SELECT Price FROM dbo.Product WHERE Id = 1), 1, 1), -- Sony phone, Miauw
(5, (SELECT Price FROM dbo.Product WHERE Id = 8), 8, 2), -- Sandberg MicroSD, Michel
(4, (SELECT Price FROM dbo.Product WHERE Id = 13), 13, 3), -- Philips Fryser, Bamse
(2, (SELECT Price FROM dbo.Product WHERE Id = 9), 9, 4) -- Dyson støvsuger, Karl

SELECT ol.Quantity, ol.Price as 'Price per item', p.Name as 'Product', ol.Quantity * p.WeightGram as 'Total weight i grams', CONVERT(date, co.OrderDate) as 'Date', CONCAT_WS(' ', c.FirstName, c.LastName) as 'Full name', (ol.Quantity * ol.Price) + (((ol.Quantity * p.WeightGram) / 1000) * co.WeightFee) + co.InitialShippingCost as 'Total shipping cost' 
FROM dbo.OrderLine ol
LEFT JOIN Product p
ON ol.Product_Id = p.Id
LEFT JOIN CustomerOrder co
ON ol.CustomerOrder_Id = co.Id
LEFT JOIN Customer c
ON co.Customer_Id = c.Id
ORDER BY ol.Price

SELECT * FROM Product
SELECT * FROM CustomerOrder

---------------- Position Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.Position;
CREATE TABLE Position
(
	Id INT NOT NULL IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL,
	CONSTRAINT PK_Position_Id PRIMARY KEY (Id)
)

--/////////////// Insert into Position Table ///////////////
INSERT INTO dbo.Position(Name)
VALUES
('Register'),
('Warehouse worker'),
('Packaging'),
('Warehouse Manager'),
('Salesman'),
('Department leader'),
('Customer service')

SELECT * FROM dbo.Position

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

--/////////////// Insert into Department Table ///////////////
INSERT INTO dbo.Department(Name, Phone, Warehouse_Id)
VALUES
('Sales Odense', '54687866', 0), --Odense lager
('HR Odense', '58996321', 0), --Odense lager
('Packing Odense', '78961123', 0), --Odense lager
('Customer service Odense', '22364899',  0), --Odense lager
('Administration Odense', '98457832', 0), --Odense lager
('Sales Berlin', '95214563', 1), --Berlin lager
('HR Berlin', '85412365', 1), --Berlin lager
('Packing Berlin', '95114266', 1), --Berlin lager
('Customer service Berlin', '87441221',  1), --Berlin lager
('Administration Berlin', '32115568', 1), --Berlin lager
('Sales Lyon', '35469987', 2), --Lyon lager
('HR Lyon', '20365985', 2), --Lyon lager
('Packing Lyon', '30526698', 2), --Lyon lager
('Customer service Lyon', '21565048',  2), --Lyon lager
('Administration Lyon', '36200355', 2), --Lyon lager
('Sales Oslo', '98752100', 3), --Oslo lager
('HR Oslo', '36541255', 3), --Oslo lager
('Packing Oslo', '69844230', 3), --Oslo lager
('Customer service Oslo', '25874123',  3), --Oslo lager
('Administration Oslo', '10254863', 3), -- Oslo lager
('Sales Moskva', '98522230', 4), --Moskva lager
('HR Moskva', '87885200', 4), --Moskva lager
('Packing Moskva', '65853400', 4), --Moskva lager
('Customer service Moskva', '35426899',  4), --Moskva lager
('Administration Moskva', '32456977', 4) -- Moskva lager

SELECT * FROM dbo.Department

SELECT * FROM Zip
SELECT * FROM Warehouse

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
	ApplicationUser_Id INT NOT NULL,
	CONSTRAINT PK_Employee_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Employee_Position FOREIGN KEY (Position_Id) REFERENCES Position(Id),
	CONSTRAINT FK_Employee_Deparment FOREIGN KEY (Department_Id) REFERENCES Department(Id),
	CONSTRAINT FK_Employee_ApplicationUser FOREIGN KEY (ApplicationUser_Id) REFERENCES ApplicationUser(Id)
)


--/////////////// Insert into Employee Table ///////////////
INSERT INTO dbo.Employee(FirstName, LastName, Email, Phone, Position_Id, Department_Id, ApplicationUser_Id)
VALUES
('Thomas', 'Josefsen', 'odense@bigshop.com', '21354685', 1, 0, 1), --Odense afdeling, warehouse worker
('Marie', 'Karlsen', 'odense@bigshop.com', '65478522', 2, 0, 1), --Odense afdeling, packaging
('Louise', 'Kofoed', 'berlin@bigshop.com', '54853612', 4, 1, 1), --Berlin afdeling, Salesman
('Karen', 'Michelin', 'berlin@bigshop.com', '85469632', 2, 1, 1), --Berlin afdeling, packaging
('Lars', 'Olsen', 'lyon@bigshop.com', '54632111', 5, 2, 1), --Lyon afdeling, Department leader
('Trevor', 'Noah', 'oslo@bigshop.com', '55689412', 1, 3, 1), --Oslo afdeling, warehouse worker
('Botan', 'Sisiro', 'moskva@bigshop.com', '44522133', 6, 4, 1), --Moskva afdeling, customer service
('Polka', 'Omaru', 'moskva@bigshop.com', '87965521', 1, 4, 1) --Moskva afdeling, warehouse worker

SELECT e.Id, CONCAT_WS(' ', e.FirstName, e.LastName) as 'Name', e.Phone, p.Name as 'Position', d.Name as 'Department'
FROM dbo.Employee e
LEFT JOIN dbo.Position p
ON e.Position_Id = p.Id
LEFT JOIN dbo.Department d
ON e.Department_Id = d.Id
ORDER BY Id


SELECT * FROM dbo.Position
SELECT * FROM dbo.Department

---------------- OrderStatus Table -------------------
--DROP TABLE IF EXISTS bigshop.dbo.OrderStatus;
CREATE TABLE OrderStatus
(
	Id INT NOT NULL IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL,
	CONSTRAINT PK_OrderStatus_Id PRIMARY KEY (Id)
)

--/////////////// Insert into OrderStatus Table ///////////////
INSERT INTO dbo.OrderStatus(Name)
VALUES
('Started processing'),
('Picked'),
('Packed'),
('Sent'),
('Store bought'),
('On hold')

SELECT * FROM dbo.OrderStatus


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

--/////////////// Insert into OrderStatus Table ///////////////
INSERT INTO dbo.OrderProcess(ProcessDate, Employee_Id, CustomerOrder_Id, OrderStatus_Id)
VALUES
((SELECT OrderDate FROM dbo.CustomerOrder WHERE Id = 0), 0, 0, 0), --warehouse worker, odense order, started processing
('2020-02-22 08:54:00', 1, 0, 2), --packaging, odense order, packed
('2020-02-22 16:30:11', 1, 0, 3), --packaging, odense order, sent
((SELECT OrderDate FROM dbo.CustomerOrder WHERE Id = 1), 3, 1, 4), --salesman, berlin order, store bought
((SELECT OrderDate FROM dbo.CustomerOrder WHERE Id = 3), 4, 3, 0), --department leader, lyon order, started processing
('2020-07-23 11:14:20', 4, 3, 1), --department leader, lyon order, picked
('2020-07-23 17:30:11', 4, 3, 2), --department leader, lyon order, packed
('2020-07-25 10:27:55', 4, 3, 3), --department leader, lyon order, sent
((SELECT OrderDate FROM dbo.CustomerOrder WHERE Id = 3), 5, 3, 0), --warehouse worker, oslo order, started processing
('2020-08-26 09:32:10', 5, 3, 1), --warehouse worker, oslo order, picked
((SELECT OrderDate FROM dbo.CustomerOrder WHERE Id = 4), 7, 4, 0), --warehouse worker, moskva order, started processing
('2021-01-29 10:45:36', 7, 4, 1), --warehouse worker, moska order, picked
('2021-01-29 15:10:44', 6, 4, 5) --customer service, moska order, on hold


----- Order process view
SELECT op.ProcessDate as 'Time of processing', op.CustomerOrder_Id as 'Order Id', CONCAT_WS(' ', e.FirstName, e.LastName) as 'Employee name', os.Name as 'Status'
FROM dbo.OrderProcess op
LEFT JOIN dbo.Employee e
ON op.Employee_Id = e.Id
LEFT JOIN dbo.OrderStatus os
ON op.OrderStatus_Id = os.Id


----- Orders view
select co.Id, co.OrderDate, z.CityName 
from CustomerOrder co
left join Customer c
on co.Customer_Id = c.Id
left join Zip z
on c.Zip_Id = z.Id

SELECT * FROM dbo.OrderStatus

SELECT e.Id, CONCAT_WS(' ', e.FirstName, e.LastName) as 'Name', e.Phone, p.Name as 'Position', d.Name as 'Department'
FROM dbo.Employee e
LEFT JOIN dbo.Position p
ON e.Position_Id = p.Id
LEFT JOIN dbo.Department d
ON e.Department_Id = d.Id
ORDER BY Id



	SELECT c.Id, c.FirstName, c.LastName, c.Phone, z.ZipCode, cn.Name 
	FROM [dbo].[Customer] c
	LEFT JOIN [dbo].[Zip] z
	ON c.[Zip_Id] = z.[Id]
	LEFT JOIN [dbo].Country cn
	ON z.[Country_Id] = cn.[Id]

