CREATE PROCEDURE [dbo].[spWarehouse_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [AddressLane], [Zip_Id]
	FROM dbo.[Warehouse]
	WHERE Id = @Id
END
