CREATE PROCEDURE [dbo].[spPosition_Insert]
	@Name VARCHAR(50),
	@Id INT OUTPUT
AS
BEGIN
	INSERT INTO dbo.[Position]([Name])
	VALUES (@Name)

	SET @Id = SCOPE_IDENTITY();
END
