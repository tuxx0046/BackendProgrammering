CREATE PROCEDURE [dbo].[spPosition_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name]
	FROM dbo.[Position]
	WHERE [Id] = @Id
END
