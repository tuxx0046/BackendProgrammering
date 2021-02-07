CREATE PROCEDURE [dbo].[spCustomer_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [FirstName], [LastName], [Phone], [AddressLane], [Zip_Id], [ApplicationUser_Id]
	FROM dbo.Customer
	WHERE [Id] = @Id
END
