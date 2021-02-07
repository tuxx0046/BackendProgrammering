CREATE PROCEDURE [dbo].[spWarehouse_Update]
	@Id INT,
	@Name VARCHAR(50),
	@AddressLane NVARCHAR(100),
	@Zip_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.[Warehouse]
	SET
		[Name] = @Name, 
		AddressLane = @AddressLane,
		Zip_Id = @Zip_Id
	WHERE Id = @Id
END