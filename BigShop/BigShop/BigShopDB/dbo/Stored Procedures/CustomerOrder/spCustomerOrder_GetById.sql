CREATE PROCEDURE [dbo].[spCustomerOrder_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT co.[Id], co.[OrderDate], co.[InitialShippingCost], co.[WeightFee], co.[Customer_Id], co.[Courier_Id], co.[PaymentMethod_Id]
	FROM dbo.[CustomerOrder] co
	WHERE co.[Id] = @Id
END