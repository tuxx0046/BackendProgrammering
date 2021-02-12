CREATE PROCEDURE [dbo].[spManufacturer_Update]
	@Id INT,
	@Name NVARCHAR(100)
AS
BEGIN
	UPDATE dbo.[Manufacturer]
	SET
		[Name] = @Name
	WHERE [Id] = @Id
END
