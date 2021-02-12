﻿CREATE PROCEDURE [dbo].[spManufacturer_Insert]
	@Name NVARCHAR(100),
	@Id INT OUTPUT
AS
BEGIN
	INSERT INTO dbo.[Manufacturer]([Name])
	VALUES (@Name)

	SET @Id = SCOPE_IDENTITY();
END
