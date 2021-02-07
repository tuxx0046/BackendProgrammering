CREATE PROCEDURE [dbo].[spWarehouse_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[Warehouse]
	WHERE Id = @Id
END
