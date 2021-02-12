CREATE PROCEDURE [dbo].[spWarehouse_Product_DeleteByWarehouseId]
	@Warehouse_Id INT
AS
BEGIN
	DELETE
	FROM dbo.[Warehouse_Product]
	WHERE Warehouse_Id = @Warehouse_Id
END