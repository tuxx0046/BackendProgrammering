CREATE PROCEDURE [dbo].[spProduct_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [Price], [EAN], [WeightGram], [Manufacturer_Id], [Category_Id]
	FROM dbo.[Product]
	WHERE [Id] = @Id
END
