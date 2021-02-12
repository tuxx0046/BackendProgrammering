CREATE PROCEDURE [dbo].[spPaymentMethod_Update]
	@Id INT,
	@Name VARCHAR(50)
AS
BEGIN
	UPDATE dbo.[PaymentMethod]
	SET
		[Name] = @Name
	WHERE [Id] = @Id
END
