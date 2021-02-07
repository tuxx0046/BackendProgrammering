CREATE PROCEDURE [dbo].[spPaymentMethod_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[PaymentMethod]
	WHERE [Id] = @Id
ENd
