CREATE PROCEDURE [dbo].[spOrderLine_GetByCustomerOrderId]
	@CustomerOrder_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Quantity], [Price], [Product_Id], [CustomerOrder_Id]
	FROM dbo.[OrderLine]
	WHERE [CustomerOrder_Id] = @CustomerOrder_Id
END
