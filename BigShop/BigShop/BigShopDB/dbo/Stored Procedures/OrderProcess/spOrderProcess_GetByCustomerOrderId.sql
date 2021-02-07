CREATE PROCEDURE [dbo].[spOrderProcess_GetByCustomerOrderId]
	@CustomerOrder_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [ProcessDate], [Employee_Id], [CustomerOrder_Id], [OrderStatus_Id]
	FROM dbo.[OrderProcess]
	WHERE [CustomerOrder_Id] = @CustomerOrder_Id
END
