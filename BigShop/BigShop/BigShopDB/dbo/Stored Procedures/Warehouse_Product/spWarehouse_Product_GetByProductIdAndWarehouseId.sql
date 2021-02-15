CREATE PROCEDURE [dbo].[spWarehouse_Product_GetByProductIdAndWarehouseId]
	@Product_Id INT,
	@Warehouse_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Quantity], [Product_Id], [Warehouse_Id]
	FROM dbo.[Warehouse_Product]
	WHERE Product_Id = @Product_Id AND Warehouse_Id = @Warehouse_Id
END
