CREATE PROCEDURE [dbo].[spCustomerOrder_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [OrderDate], [InitialShippingCost], [WeightFee], [Customer_Id], [Courier_Id], [PaymentMethod_Id]
	FROM dbo.[CustomerOrder]
END
