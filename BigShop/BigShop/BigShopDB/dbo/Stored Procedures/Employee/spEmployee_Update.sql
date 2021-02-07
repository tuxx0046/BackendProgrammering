CREATE PROCEDURE [dbo].[spEmployee_Update]
	@Id INT,
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(100),
	@Phone VARCHAR(50),
	@Position_Id INT,
	@Department_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.[Employee]
	SET 
		[FirstName] = @FirstName,
		[LastName] = @LastName,
		[Phone] = @Phone,
		[Position_Id] = @Position_Id,
		[Department_Id] = @Department_Id
	WHERE [Id] = @Id
END

