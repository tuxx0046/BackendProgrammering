CREATE PROCEDURE [dbo].[spDepartment_Insert]
	@Name VARCHAR(50),
	@Phone VARCHAR(50),
	@Warehouse_Id INT,
	@Id INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.[Department]([Name], [Phone], [Warehouse_Id])
	VALUES (@Name, @Phone, @Warehouse_Id)

	SET @Id = SCOPE_IDENTITY();
END