CREATE PROCEDURE [dbo].[spOrderProcess_Insert]
	@Employee_Id INT,
	@CustomerOrder_Id INT,
	@OrderStatus_Id INT
AS
BEGIN
	INSERT INTO dbo.[OrderProcess]([Employee_Id], [CustomerOrder_Id], [OrderStatus_Id])
	VALUES (@Employee_Id, @CustomerOrder_Id, @OrderStatus_Id)
END