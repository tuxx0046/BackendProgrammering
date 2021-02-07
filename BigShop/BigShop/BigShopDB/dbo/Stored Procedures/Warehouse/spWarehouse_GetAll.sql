CREATE PROCEDURE [dbo].[spWarehouse_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [AddressLane], [Zip_Id]
	FROM dbo.[Warehouse]
END