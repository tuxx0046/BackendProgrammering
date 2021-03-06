﻿CREATE PROCEDURE [dbo].[spApplicationUser_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Username], [NormalizedUsername], [Email], [NormalizedEmail], [PasswordHash]
	FROM dbo.[ApplicationUser]
	WHERE Id = @Id
END
