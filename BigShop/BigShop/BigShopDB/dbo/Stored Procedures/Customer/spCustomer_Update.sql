CREATE PROCEDURE [dbo].[spCustomer_Update]
	@Id INT,
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Phone VARCHAR(50),
	@AddressLane NVARCHAR(100),
	@Zip_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.[Customer]
	SET 
		[FirstName] = @FirstName,
		[LastName] = @LastName, 
		[Phone] = @Phone, 
		[AddressLane] =  @AddressLane,
		[Zip_Id] = @Zip_Id
	WHERE [Id] = @Id
END