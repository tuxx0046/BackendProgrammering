CREATE PROCEDURE [dbo].[spZip_Update]
	@Id INT,
	@ZipCode VARCHAR(50),
	@CityName NVARCHAR(100),
	@Country_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.[Zip]
	SET 
		ZipCode = @ZipCode, 
		CityName = @CityName, 
		Country_Id = @Country_Id
	WHERE Id = @Id
END
