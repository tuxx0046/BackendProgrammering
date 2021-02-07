CREATE PROCEDURE [dbo].[spOrderStatus_GetAll]
AS
BEGIN
	SELECT [Id], [Name]
	FROM dbo.[OrderStatus]
END
