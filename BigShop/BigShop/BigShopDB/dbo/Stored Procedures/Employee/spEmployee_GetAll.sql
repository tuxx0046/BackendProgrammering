CREATE PROCEDURE [dbo].[spEmployee_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [FirstName], [LastName], [Phone], [Position_Id], [Department_Id]
	FROM dbo.[Employee]
END
