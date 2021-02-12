CREATE PROCEDURE [dbo].[spZip_Insert]
	@ZipCode VARCHAR(50),
	@CityName NVARCHAR(100),
	@Country_Id INT,
	@Id INT OUTPUT
AS
BEGIN
	INSERT INTO dbo.[Zip](ZipCode, CityName, Country_Id)
	VALUES (@ZipCode, @CityName, @Country_Id)

	SET @Id = SCOPE_IDENTITY();
END
