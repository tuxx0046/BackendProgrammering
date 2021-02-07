CREATE PROCEDURE [dbo].[spOrderStatus_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name]
	FROM dbo.[OrderStatus]
	WHERE [Id] = @Id
END
