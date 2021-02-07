CREATE PROCEDURE [dbo].[spCourier_Insert]
	@Name VARCHAR(50),
	@InitialCost DECIMAL(7,2),
	@WeightFee DECIMAL(7,2),
	@Id INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.[Courier]([Name], [InitialCost], [WeightFee])
	VALUES (@Name, @InitialCost, @WeightFee)

	SET @Id = SCOPE_IDENTITY();
END
