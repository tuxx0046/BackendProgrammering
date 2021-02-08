CREATE PROCEDURE [dbo].[spApplicationUser_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Username], [Email]
	FROM dbo.[ApplicationUser]
	WHERE Id = @Id
END
