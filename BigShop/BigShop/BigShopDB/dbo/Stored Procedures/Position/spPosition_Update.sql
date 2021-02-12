CREATE PROCEDURE [dbo].[spPosition_Update]
	@Id INT,
	@Name VARCHAR(50)
AS
BEGIN
	UPDATE dbo.[Position]
	SET
		[Name] = @Name
	WHERE [Id] = @Id
END
