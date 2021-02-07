CREATE PROCEDURE [dbo].[spWarehouse_Product_DeleteByWarehouseId]
	@Warehouse_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[Warehouse_Product]
	WHERE Warehouse_Id = @Warehouse_Id
END