CREATE PROCEDURE [dbo].[spCustomerOrder_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[CustomerOrder]
	WHERE [Id] = @Id
END





