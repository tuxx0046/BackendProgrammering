CREATE PROCEDURE [dbo].[spOrderProcess_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [ProcessDate], [Employee_Id], [CustomerOrder_Id], [OrderStatus_Id]
	FROM dbo.[OrderProcess]
END
