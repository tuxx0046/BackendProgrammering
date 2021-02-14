CREATE PROCEDURE [dbo].[spOrderLine_GetByProductId]
	@Product_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Quantity], [Price], [Product_Id], [CustomerOrder_Id]
	FROM dbo.[OrderLine]
	WHERE [Product_Id] = @Product_Id
END