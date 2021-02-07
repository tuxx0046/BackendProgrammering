CREATE PROCEDURE [dbo].[spEmployee_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[Employee]
	WHERE [Id] = @Id
END
