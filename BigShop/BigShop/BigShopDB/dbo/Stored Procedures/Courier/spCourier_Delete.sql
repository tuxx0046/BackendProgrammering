CREATE PROCEDURE [dbo].[spCourier_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM dbo.[Courier]
	WHERE [Id] = @Id
END
