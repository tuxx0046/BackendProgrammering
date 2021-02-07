CREATE PROCEDURE [dbo].[spDepartment_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [Phone], [Warehouse_Id]
	FROM dbo.[Department]
	WHERE [Id] = @Id
END