CREATE PROCEDURE [dbo].[spDepartment_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [Phone], [Warehouse_Id]
	FROM dbo.[Department]
END
