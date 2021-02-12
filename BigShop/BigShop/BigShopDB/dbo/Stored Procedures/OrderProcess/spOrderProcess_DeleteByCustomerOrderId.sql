CREATE PROCEDURE [dbo].[spOrderProcess_DeleteByCustomerOrderId]
	@CustomerOrder_Id INT
AS
BEGIN
	DELETE
	FROM dbo.[OrderProcess]
	WHERE [CustomerOrder_Id] = @CustomerOrder_Id
END
