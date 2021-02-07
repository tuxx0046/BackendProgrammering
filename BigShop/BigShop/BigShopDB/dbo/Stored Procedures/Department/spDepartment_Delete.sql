CREATE PROCEDURE [dbo].[spDepartment_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[Department]
	WHERE [Id] = @Id
END
