CREATE PROCEDURE [dbo].[spApplicationUser_Update]
	@Id INT,
	@Username VARCHAR(20),
	@NormalizedUsername VARCHAR(20),
	@Email VARCHAR(50),
	@NormalizedEmail VARCHAR(50),
	@PasswordHash NVARCHAR(MAX)
AS
BEGIN
	UPDATE dbo.[ApplicationUser]
	SET
		Username = @Username, 
		NormalizedUsername = @NormalizedUsername, 
		Email = @Email, 
		NormalizedEmail = @NormalizedEmail, 
		PasswordHash = @PasswordHash
	WHERE Id = @Id
END