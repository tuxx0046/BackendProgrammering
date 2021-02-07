CREATE PROCEDURE [dbo].[spProduct_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [Price], [EAN], [WeightGram], [Manufacturer_Id], [Category_Id]
	FROM dbo.[Product]
END
