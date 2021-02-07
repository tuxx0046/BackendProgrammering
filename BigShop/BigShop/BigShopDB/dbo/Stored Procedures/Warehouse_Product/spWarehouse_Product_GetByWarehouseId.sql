CREATE PROCEDURE [dbo].[spWarehouse_Product_GetByWarehouseId]
	@Warehouse_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Quantity], [Product_Id], [Warehouse_Id]
	FROM dbo.[Warehouse_Product]
	WHERE Warehouse_Id = @Warehouse_Id
END
