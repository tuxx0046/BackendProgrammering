CREATE PROCEDURE [dbo].[spCategory_Insert]
	@Name NVARCHAR(50),
	@Id INT OUTPUT
AS
BEGIN
	INSERT INTO dbo.[Category]([Name])
	VALUES (@Name)

	SET @Id = SCOPE_IDENTITY();
END
