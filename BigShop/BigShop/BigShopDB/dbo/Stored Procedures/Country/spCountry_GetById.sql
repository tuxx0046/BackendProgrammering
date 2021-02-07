CREATE PROCEDURE [dbo].[spCountry_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name]
	FROM dbo.[Country]
	WHERE [Id] = @Id
END
