CREATE PROCEDURE [dbo].[spManufacturer_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[Manufacturer]
	WHERE [Id] = @Id
END
