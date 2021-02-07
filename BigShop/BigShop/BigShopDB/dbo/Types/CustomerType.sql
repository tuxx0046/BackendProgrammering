CREATE TYPE [dbo].[CustomerType] AS TABLE
(
	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(100) NOT NULL,
	[Email] VARCHAR(70) NOT NULL,
	[NormalizedEmail] VARCHAR(70) NULL,
	[Phone] VARCHAR(50) NOT NULL,
	[AddressLane] NVARCHAR(100) NOT NULL,
	[Zip_Id] INT NOT NULL
)
