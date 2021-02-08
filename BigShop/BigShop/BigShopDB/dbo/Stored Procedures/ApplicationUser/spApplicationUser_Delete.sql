CREATE PROCEDURE [dbo].[spApplicationUser_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[ApplicationUser]
	WHERE Id = @Id
END
