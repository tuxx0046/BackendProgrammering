CREATE PROCEDURE [dbo].[spDepartment_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[Department]
	WHERE [Id] = @Id
END
