CREATE PROCEDURE [dbo].[spPaymentMethod_Insert]
	@Name VARCHAR(50),
	@Id INT OUTPUT
AS
BEGIN
	INSERT INTO dbo.[PaymentMethod]([Name])
	VALUES (@Name)

	SET @Id = SCOPE_IDENTITY();
END
