CREATE PROCEDURE [dbo].[spProduct_GetByManufacturerId]
	@Manufacturer_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [Price], [EAN], [WeightGram], [Manufacturer_Id], [Category_Id]
	FROM dbo.[Product]
	WHERE [Manufacturer_Id] = @Manufacturer_Id
END