CREATE PROCEDURE [dbo].[spOrderStatus_Insert]
	@Name VARCHAR(50),
	@Id INT OUTPUT
AS
BEGIN
	INSERT INTO dbo.[OrderStatus]([Name])
	VALUES (@Name)

	SET @Id = SCOPE_IDENTITY();
END
