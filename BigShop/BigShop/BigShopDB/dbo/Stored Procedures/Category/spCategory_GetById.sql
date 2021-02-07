CREATE PROCEDURE [dbo].[spCategory_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name]
	FROM dbo.[Category]
	WHERE [Id] = @Id
END
