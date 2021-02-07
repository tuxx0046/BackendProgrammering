CREATE PROCEDURE [dbo].[spCustomer_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [FirstName], [LastName], [Phone], [AddressLane], [Zip_Id], [ApplicationUser_Id]
	FROM dbo.Customer
END
