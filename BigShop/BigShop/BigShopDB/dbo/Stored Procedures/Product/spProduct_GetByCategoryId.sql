CREATE PROCEDURE [dbo].[spProduct_GetByCategoryId]
	@Category_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [Price], [EAN], [WeightGram], [Manufacturer_Id], [Category_Id]
	FROM dbo.[Product]
	WHERE [Category_Id] = @Category_Id
END