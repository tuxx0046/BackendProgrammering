CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL IDENTITY(0,1),
	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(100) NOT NULL,
	[Phone] VARCHAR(50) NULL,
	[Position_Id] INT NOT NULL,
	[Department_Id] INT NOT NULL,
	[ApplicationUser_Id] INT NOT NULL,
	CONSTRAINT PK_Employee_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Employee_Position FOREIGN KEY (Position_Id) REFERENCES Position(Id),
	CONSTRAINT FK_Employee_Deparment FOREIGN KEY (Department_Id) REFERENCES Department(Id),
	CONSTRAINT FK_Employee_ApplicationUser FOREIGN KEY (ApplicationUser_Id) REFERENCES ApplicationUser(Id)
)
