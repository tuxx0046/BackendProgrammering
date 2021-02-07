CREATE PROCEDURE [dbo].[spCustomerOrder_GetAllByCustomerId]
	@Customer_Id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT co.[Id], co.[OrderDate], co.[InitialShippingCost], co.[WeightFee], co.[Customer_Id], co.[Courier_Id], co.[PaymentMethod_Id]
	FROM dbo.[CustomerOrder] co
	LEFT JOIN dbo.[Customer] c
	ON co.[Customer_Id] = c.[Id]
	WHERE c.[Id] = @Customer_Id
END
