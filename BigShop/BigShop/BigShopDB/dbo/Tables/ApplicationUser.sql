CREATE TABLE [dbo].[ApplicationUser]
(
	[Id] INT NOT NULL IDENTITY(0,1),
	[Username] VARCHAR(20) NULL,
	[NormalizedUsername] VARCHAR(20) NULL,
	[Email] VARCHAR(50) NOT NULL,
	[NormalizedEmail] VARCHAR(50) NULL,
	[PasswordHash] NVARCHAR(MAX) NULL,
	CONSTRAINT PK_ApplicationUser_Id PRIMARY KEY (Id)
)
