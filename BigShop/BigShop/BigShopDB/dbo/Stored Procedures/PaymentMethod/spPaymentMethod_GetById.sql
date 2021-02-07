CREATE PROCEDURE [dbo].[spPaymentMethod_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name]
	FROM dbo.[PaymentMethod]
	WHERE [Id] = @Id
END
