CREATE PROCEDURE [dbo].[spCourier_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [InitialCost], [WeightFee]
	FROM dbo.[Courier]
	WHERE [Id] = @Id
END
