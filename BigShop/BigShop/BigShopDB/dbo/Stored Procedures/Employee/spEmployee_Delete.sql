CREATE PROCEDURE [dbo].[spEmployee_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[Employee]
	WHERE [Id] = @Id
END
