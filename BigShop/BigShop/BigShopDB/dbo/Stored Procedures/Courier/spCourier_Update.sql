CREATE PROCEDURE [dbo].[spCourier_UpdateName]
	@Id INT,
	@Name VARCHAR(50),
	@InitialCost DECIMAL(7,2),
	@WeightFee DECIMAL(7,2)
AS
BEGIN
	UPDATE  dbo.[Courier]
	SET 
		[Name] = @Name,
		[InitialCost] = @InitialCost,
		[WeightFee] = @WeightFee
	WHERE [Id] = @Id
END