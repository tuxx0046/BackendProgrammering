CREATE PROCEDURE [dbo].[spWarehouse_Insert]
	@Name VARCHAR(50),
	@AddressLane NVARCHAR(100),
	@Zip_Id INT,
	@Id INT OUTPUT
AS
BEGIN
	INSERT INTO dbo.[Warehouse]([Name], AddressLane, Zip_Id)
	VALUES (@Name, @AddressLane, @Zip_Id)

	SET @Id = SCOPE_IDENTITY();
END
