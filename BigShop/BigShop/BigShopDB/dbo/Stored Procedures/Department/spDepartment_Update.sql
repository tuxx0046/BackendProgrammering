CREATE PROCEDURE [dbo].[spDepartment_Update]
	@Id INT,
	@Name VARCHAR(50),
	@Phone VARCHAR(50),
	@Warehouse_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.[Department]
	SET
		[Name] = @Name,
		[Phone] = @Phone,
		[Warehouse_Id] = @Warehouse_Id
	WHERE [Id] = @Id
END