CREATE PROCEDURE [dbo].[spCategory_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT c.[Id], c.[Name]
	FROM [dbo].[Category] c
END
