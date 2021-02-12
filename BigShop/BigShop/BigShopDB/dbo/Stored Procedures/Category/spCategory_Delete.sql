CREATE PROCEDURE [dbo].[spCategory_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[Category]
	WHERE [Id] = @Id;
END