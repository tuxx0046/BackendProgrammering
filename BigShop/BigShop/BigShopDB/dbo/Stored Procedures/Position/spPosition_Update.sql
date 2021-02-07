CREATE PROCEDURE [dbo].[spPosition_Update]
	@Id INT,
	@Name VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.[Position]
	SET
		[Name] = @Name
	WHERE [Id] = @Id
END
