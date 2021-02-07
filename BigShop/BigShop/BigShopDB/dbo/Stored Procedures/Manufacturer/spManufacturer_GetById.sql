CREATE PROCEDURE [dbo].[spManufacturer_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name]
	FROM dbo.[Manufacturer]
	WHERE [Id] = @Id
ENd
