CREATE PROCEDURE [dbo].[spProduct_Update]
	@Id INT,
	@Name NVARCHAR(150),
	@Price DECIMAL(7,2),
	@EAN VARCHAR(13),
	@WeightGram INT,
	@Manufacturer_Id INT,
	@Category_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.[Product]
	SET
		[Name] = @Name,
		Price = @Price,
		EAN = @EAN,
		WeightGram = @WeightGram,
		Manufacturer_Id = @Manufacturer_Id,
		Category_Id = @Category_Id
	WHERE Id = @Id
END
