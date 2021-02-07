CREATE PROCEDURE [dbo].[spWarehouse_Product_GetByProductId]
	@Product_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Quantity], [Product_Id], [Warehouse_Id]
	FROM dbo.[Warehouse_Product]
	WHERE Product_Id = @Product_Id
END
