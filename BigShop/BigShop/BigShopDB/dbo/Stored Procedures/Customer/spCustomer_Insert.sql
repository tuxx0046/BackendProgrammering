CREATE PROCEDURE [dbo].[spCustomer_Insert]
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Phone VARCHAR(50),
	@AddressLane NVARCHAR(100),
	@Zip_Id INT,
	@ApplicationUser_Id INT,
	@Id INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.[Customer]([FirstName], [LastName], [Phone], [AddressLane], [Zip_Id], [ApplicationUser_Id])
	VALUES (@FirstName, @LastName, @Phone, @AddressLane, @Zip_Id, @ApplicationUser_Id)

	SET @Id = SCOPE_IDENTITY();
END