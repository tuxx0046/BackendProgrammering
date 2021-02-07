CREATE PROCEDURE [dbo].[spEmployee_Insert]
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(100),
	@Phone VARCHAR(50),
	@Position_Id INT,
	@Department_Id INT,
	@ApplicationUser_Id INT,
	@Id INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO dbo.[Employee]([FirstName], [LastName], [Phone], [Position_Id], [Department_Id], [ApplicationUser_Id])
	VALUES (@FirstName, @LastName, @Phone, @Position_Id, @Department_Id, @ApplicationUser_Id)

	SET @Id = SCOPE_IDENTITY();
END