CREATE TABLE [dbo].[OrderProcess]
(
	[ProcessDate] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[Employee_Id] INT NULL,
	[CustomerOrder_Id] BIGINT NOT NULL,
	[OrderStatus_Id] INT NOT NULL DEFAULT 0,
	CONSTRAINT FK_OrderProcess_Employee FOREIGN KEY (Employee_Id) REFERENCES Employee(Id),
	CONSTRAINT FK_OrderProcess_CustomerOrder FOREIGN KEY (CustomerOrder_Id) REFERENCES CustomerOrder(Id),
	CONSTRAINT FK_OrderProcess_OrderStatus FOREIGN KEY (OrderStatus_Id) REFERENCES OrderStatus(Id)
)
