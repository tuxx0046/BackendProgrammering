CREATE PROCEDURE [dbo].[spOrderStatus_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[OrderStatus]
	WHERE Id = @Id
END
