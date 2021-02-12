CREATE PROCEDURE [dbo].[spProduct_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[Product]
	WHERE Id = @Id
END
