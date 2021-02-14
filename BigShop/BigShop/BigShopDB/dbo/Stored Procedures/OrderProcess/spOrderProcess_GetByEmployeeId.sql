CREATE PROCEDURE [dbo].[spOrderProcess_GetByEmployeeId]
	@Employee_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [ProcessDate], [Employee_Id], [CustomerOrder_Id], [OrderStatus_Id]
	FROM dbo.[OrderProcess]
	WHERE [Employee_Id] = @Employee_Id
END