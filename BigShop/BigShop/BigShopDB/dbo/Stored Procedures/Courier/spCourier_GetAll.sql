CREATE PROCEDURE [dbo].[spCourier_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [InitialCost], [WeightFee]
	FROM [dbo].[Courier]
END
