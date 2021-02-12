CREATE PROCEDURE [dbo].[spCountry_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[Country]
	WHERE [Id] = @Id;
END
