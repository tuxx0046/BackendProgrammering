CREATE TABLE [dbo].[OrderLine]
(
	[Quantity] INT NOT NULL,
	[Price] DECIMAL(7,2) NOT NULL,
	[Product_Id] INT NOT NULL,
	[CustomerOrder_Id] BIGINT NOT NULL,
	CONSTRAINT FK_OrderLine_Product FOREIGN KEY (Product_Id) REFERENCES Product(Id),
	CONSTRAINT FK_OrderLine_CustomerOrder FOREIGN KEY (CustomerOrder_Id) REFERENCES CustomerOrder(Id)
)
