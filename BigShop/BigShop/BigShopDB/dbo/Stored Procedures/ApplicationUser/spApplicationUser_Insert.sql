CREATE PROCEDURE [dbo].[spApplicationUser_Insert]
	@Username VARCHAR(20),
	@NormalizedUsername VARCHAR(20),
	@Email VARCHAR(50),
	@NormalizedEmail VARCHAR(50),
	@PasswordHash NVARCHAR(MAX)
	--@Id INT OUTPUT
AS
BEGIN
	INSERT INTO dbo.[ApplicationUser](Username, NormalizedUsername, Email, NormalizedEmail, PasswordHash)
	VALUES (@Username, @NormalizedUsername, @Email, @NormalizedEmail, @PasswordHash)

	--SET @Id = SCOPE_IDENTITY();
END