CREATE PROCEDURE [dbo].[spCustomer_GetByApplicationUserId]
	--@ApplicationUser_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [FirstName], [LastName], [Phone], [AddressLane], [Zip_Id]
	FROM dbo.[Customer]
	--WHERE ApplicationUser_Id = @ApplicationUser_Id
END
