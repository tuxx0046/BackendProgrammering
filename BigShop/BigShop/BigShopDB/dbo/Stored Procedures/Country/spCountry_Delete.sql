CREATE PROCEDURE [dbo].[spCountry_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[Country]
	WHERE [Id] = @Id;
END
