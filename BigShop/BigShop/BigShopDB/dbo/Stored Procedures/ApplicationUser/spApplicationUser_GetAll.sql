CREATE PROCEDURE [dbo].[spApplicationUser_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Username], [Email]
	FROM dbo.[ApplicationUser]
END
