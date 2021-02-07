CREATE PROCEDURE [dbo].[spCountry_Insert]
	@Name NVARCHAR(100),
	@Id INT OUTPUT
AS
BEGIN
	INSERT INTO dbo.[Country]([Name])
	VALUES (@Name)

	SET @Id = SCOPE_IDENTITY();
END