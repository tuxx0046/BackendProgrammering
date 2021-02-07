CREATE PROCEDURE [dbo].[spCustomerOrder_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[CustomerOrder]
	WHERE [Id] = @Id
END





