CREATE PROCEDURE [dbo].[spWarehouse_Product_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Quantity], [Product_Id], [Warehouse_Id]
	FROM dbo.[Warehouse_Product]
END
