CREATE PROCEDURE [dbo].[spOrderLine_Insert]
	@Quantity INT,
	@Price DECIMAL(7,2),
	@Product_Id INT,
	@CustomerOrder_Id INT
AS
BEGIN
	INSERT INTO dbo.[OrderLine]([Quantity], [Price], [Product_Id], [CustomerOrder_Id])
	VALUES (@Quantity, @Price, @Product_Id, @CustomerOrder_Id)
END