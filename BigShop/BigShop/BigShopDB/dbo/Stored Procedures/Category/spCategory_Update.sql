CREATE PROCEDURE [dbo].[spCategory_Update]
	@Id INT,
	@Name NVARCHAR(50)
AS
BEGIN
	UPDATE dbo.[Category]
	SET [Name] = @Name
	WHERE [Id] = @Id
END
