﻿CREATE TABLE [dbo].[OrderStatus]
(
	[Id] INT NOT NULL IDENTITY(0,1),
	[Name] VARCHAR(50) NOT NULL,
	CONSTRAINT PK_OrderStatus_Id PRIMARY KEY (Id)
)
