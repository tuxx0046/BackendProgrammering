CREATE PROCEDURE [dbo].[spPaymentMethod_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[PaymentMethod]
	WHERE [Id] = @Id
ENd
