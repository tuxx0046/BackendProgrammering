CREATE PROCEDURE [dbo].[spOrderLine_DeleteByCustomerOrderId]
	@CustomerOrder_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[OrderLine]
	WHERE [CustomerOrder_Id] = @CustomerOrder_Id
END
