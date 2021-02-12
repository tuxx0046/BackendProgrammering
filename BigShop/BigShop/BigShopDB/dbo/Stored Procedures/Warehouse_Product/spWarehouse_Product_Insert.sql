CREATE PROCEDURE [dbo].[spWarehouse_Product_Insert]
	@Quantity INT,
	@Product_Id INT,
	@Warehouse_Id INT
AS
BEGIN
	INSERT INTO dbo.[Warehouse_Product](Quantity, Product_Id, Warehouse_Id)
	VALUES (@Quantity, @Product_Id, @Warehouse_Id)
END