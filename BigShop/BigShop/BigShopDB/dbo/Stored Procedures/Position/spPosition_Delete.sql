CREATE PROCEDURE [dbo].[spPosition_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[Position]
	WHERE [Id] = @Id
ENd
