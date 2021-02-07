CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL IDENTITY(0,1),
	[Name] NVARCHAR(150) NOT NULL,
	[Price] DECIMAL(7,2) NOT NULL,
	[EAN] VARCHAR(13) NULL,
	[WeightGram] INT NOT NULL,
	[Manufacturer_Id] INT NOT NULL,
	[Category_Id] INT NOT NULL,
	CONSTRAINT PK_Product_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Product_Manufacturer FOREIGN KEY (Manufacturer_Id) REFERENCES Manufacturer(Id),
	CONSTRAINT FK_Product_Category FOREIGN KEY (Category_Id) REFERENCES Category(Id)
)
