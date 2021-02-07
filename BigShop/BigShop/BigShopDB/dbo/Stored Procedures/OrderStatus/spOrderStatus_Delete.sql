CREATE PROCEDURE [dbo].[spOrderStatus_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[OrderStatus]
	WHERE Id = @Id
END
