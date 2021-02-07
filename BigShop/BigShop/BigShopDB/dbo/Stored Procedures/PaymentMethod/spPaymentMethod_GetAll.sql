CREATE PROCEDURE [dbo].[spPaymentMethod_GetAll]
AS
BEGIN
	SELECT [Id], [Name]
	FROM dbo.[PaymentMethod]
END
