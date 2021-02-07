CREATE PROCEDURE [dbo].[spCategory_Update]
	@Id INT,
	@Name NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.[Category]
	SET [Name] = @Name
	WHERE [Id] = @Id
END
