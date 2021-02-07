CREATE PROCEDURE [dbo].[spPosition_GetAll]
AS
BEGIN
	SELECT [Id], [Name]
	FROM dbo.[Position]
END
