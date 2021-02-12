CREATE PROCEDURE [dbo].[spWarehouse_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[Warehouse]
	WHERE Id = @Id
END
