CREATE PROCEDURE [dbo].[spZip_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[Zip]
	WHERE Id = @Id
END
