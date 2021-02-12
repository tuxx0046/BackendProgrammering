CREATE PROCEDURE [dbo].[spCustomerOrder_Insert]
	@InitialShippingCost DECIMAL(7,2),
	@WeightFee DECIMAL(7,2),
	@Customer_Id INT,
	@Courier_Id INT,
	@PaymentMethod_Id INT,
	@Id INT OUTPUT
AS
BEGIN
	INSERT INTO dbo.[CustomerOrder]([InitialShippingCost], [WeightFee], [Customer_Id], [Courier_Id], [PaymentMethod_Id])
	VALUES (@InitialShippingCost, @WeightFee, @Customer_Id, @Courier_Id, @PaymentMethod_Id)

	SET @Id = SCOPE_IDENTITY();
END
