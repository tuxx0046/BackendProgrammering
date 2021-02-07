CREATE PROCEDURE [dbo].[spCountry_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT c.[Id], c.[Name]
	FROM [dbo].[Country] c
END
