CREATE TABLE [dbo].[CustomerOrder]
(
	[Id] BIGINT NOT NULL IDENTITY(0,1),
	[OrderDate] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[InitialShippingCost] DECIMAL(7,2) NOT NULL,
	[WeightFee] DECIMAL(7,2) NOT NULL,
	[Customer_Id] INT NOT NULL,
	[Courier_Id] INT NOT NULL,
	[PaymentMethod_Id] INT NOT NULL,
	CONSTRAINT PK_Order_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Order_Customer FOREIGN KEY (Customer_Id) REFERENCES Customer(Id),
	CONSTRAINT FK_Order_Courier FOREIGN KEY (Courier_Id) REFERENCES Courier(Id),
	CONSTRAINT FK_Order_PaymentMethod FOREIGN KEY (PaymentMethod_Id) REFERENCES PaymentMethod(Id)
)
