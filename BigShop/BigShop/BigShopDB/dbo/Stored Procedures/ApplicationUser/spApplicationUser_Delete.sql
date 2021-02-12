CREATE PROCEDURE [dbo].[spApplicationUser_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[ApplicationUser]
	WHERE Id = @Id
END
