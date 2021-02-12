CREATE PROCEDURE [dbo].[spProduct_Insert]
	@Name NVARCHAR(150),
	@Price DECIMAL(7,2),
	@EAN VARCHAR(13),
	@WeightGram INT,
	@Manufacturer_Id INT,
	@Category_Id INT,
	@Id INT OUTPUT
AS
BEGIN
	INSERT INTO dbo.[Product]([Name], Price, EAN, WeightGram, Manufacturer_Id, Category_Id)
	VALUES (@Name, @Price, @EAN, @WeightGram, @Manufacturer_Id, @Category_Id)
	
	SET @Id = SCOPE_IDENTITY();
END