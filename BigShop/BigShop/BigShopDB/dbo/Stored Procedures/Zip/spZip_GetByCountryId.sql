CREATE PROCEDURE [dbo].[spZip_GetByCountryId]
	@Country_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [ZipCode], [CityName], [Country_Id]
	FROM dbo.[Zip]
	WHERE Country_Id = @Country_Id
END
