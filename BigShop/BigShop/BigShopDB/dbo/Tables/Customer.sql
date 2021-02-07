﻿CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL IDENTITY(0,1),
	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(100) NOT NULL,
	[Phone] VARCHAR(50) NOT NULL,
	[AddressLane] NVARCHAR(100) NOT NULL,
	[Zip_Id] INT NOT NULL,
	[ApplicationUser_Id] INT NOT NULL,
	CONSTRAINT PK_Customer_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Customer_Zip FOREIGN KEY (Zip_Id) REFERENCES Zip(Id),
	CONSTRAINT FK_Customer_ApplicationUser FOREIGN KEY (ApplicationUser_Id) REFERENCES ApplicationUser(Id)
)
