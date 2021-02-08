CREATE PROCEDURE [dbo].[spZip_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [ZipCode], [CityName], [Country_Id]
	FROM dbo.[Zip]
	WHERE Id = @Id
END
