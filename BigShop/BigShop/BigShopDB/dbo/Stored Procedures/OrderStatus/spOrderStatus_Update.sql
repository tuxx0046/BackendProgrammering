CREATE PROCEDURE [dbo].[spOrderStatus_Update]
	@Id INT,
	@Name VARCHAR(50)
AS
BEGIN
	UPDATE dbo.[OrderStatus]
	SET
		[Name] = @Name
	WHERE [Id] = @Id
END
