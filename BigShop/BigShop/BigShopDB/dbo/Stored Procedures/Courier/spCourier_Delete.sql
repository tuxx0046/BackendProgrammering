CREATE PROCEDURE [dbo].[spCourier_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
	FROM dbo.[Courier]
	WHERE [Id] = @Id
END
