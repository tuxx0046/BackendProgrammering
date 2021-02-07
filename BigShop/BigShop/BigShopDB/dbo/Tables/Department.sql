CREATE TABLE [dbo].[Department]
(
	[Id] INT NOT NULL IDENTITY(0,1),
	[Name] VARCHAR(50) NOT NULL,
	[Phone] VARCHAR(50) NULL,
	[Warehouse_Id] INT NOT NULL,
	CONSTRAINT PK_Department_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Department_Warehouse FOREIGN KEY (Warehouse_Id) REFERENCES Warehouse(Id)
)
