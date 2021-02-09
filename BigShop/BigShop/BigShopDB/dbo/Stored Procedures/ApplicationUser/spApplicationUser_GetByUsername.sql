CREATE PROCEDURE [dbo].[spApplicationUser_GetByUsername]
	@NormalizedUsername VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Username], [NormalizedUsername], [Email], [NormalizedEmail], [PasswordHash]
	FROM dbo.[ApplicationUser]
	WHERE NormalizedUsername = @NormalizedUsername
END
