CREATE TABLE [dbo].[Warehouse_Product]
(
	[Quantity] INT NOT NULL,
	[Product_Id] INT NOT NULL,
	[Warehouse_Id] INT NOT NULL,
	CONSTRAINT FK_Warehouse_Product_Product FOREIGN KEY (Product_Id) REFERENCES Product(Id),
	CONSTRAINT FK_Warehouse_Product_Warehouse FOREIGN KEY (Warehouse_Id) REFERENCES Warehouse(Id)
)
