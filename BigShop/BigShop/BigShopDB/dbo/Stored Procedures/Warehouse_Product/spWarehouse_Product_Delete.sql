CREATE PROCEDURE [dbo].[spWarehouse_Product_DeleteByProductId]
	@Product_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[Warehouse_Product]
	WHERE Product_Id = @Product_Id
END
