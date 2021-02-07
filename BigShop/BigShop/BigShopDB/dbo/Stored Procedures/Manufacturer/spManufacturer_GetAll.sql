CREATE PROCEDURE [dbo].[spManufacturer_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name]
	FROM dbo.[Manufacturer]
END
