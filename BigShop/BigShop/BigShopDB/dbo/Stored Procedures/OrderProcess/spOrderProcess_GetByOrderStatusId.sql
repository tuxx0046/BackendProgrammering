CREATE PROCEDURE [dbo].[spOrderProcess_GetByOrderStatusId]
	@OrderStatus_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [ProcessDate], [Employee_Id], [CustomerOrder_Id], [OrderStatus_Id]
	FROM dbo.[OrderProcess]
	WHERE [OrderStatus_Id] = @OrderStatus_Id
END