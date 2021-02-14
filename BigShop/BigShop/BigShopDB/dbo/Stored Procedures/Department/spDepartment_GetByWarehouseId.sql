CREATE PROCEDURE [dbo].[spDepartment_GetByWarehouseId]
	@Warehouse_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [Phone], [Warehouse_Id]
	FROM dbo.[Department]
	WHERE [Warehouse_Id] = @Warehouse_Id
END
