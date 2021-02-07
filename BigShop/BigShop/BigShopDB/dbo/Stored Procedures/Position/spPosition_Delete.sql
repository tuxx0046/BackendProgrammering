CREATE PROCEDURE [dbo].[spPosition_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[Position]
	WHERE [Id] = @Id
ENd
