CREATE PROCEDURE [dbo].[spWarehouse_Product_UpdateQuantity]
	@Quantity INT,
	@Product_Id INT,
	@Warehouse_Id INT
AS
BEGIN
	UPDATE dbo.[Warehouse_Product]
	SET
		Quantity = @Quantity
	WHERE Product_Id = @Product_Id
	AND Warehouse_Id = @Warehouse_Id
END