CREATE PROCEDURE [dbo].[spManufacturer_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[Manufacturer]
	WHERE [Id] = @Id
END
