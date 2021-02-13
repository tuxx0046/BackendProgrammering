CREATE PROCEDURE [dbo].[spEmployee_GetByDepartmentId]
	@Department_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [FirstName], [LastName], [Phone], [Position_Id], [Department_Id]
	FROM dbo.[Employee]
	WHERE [Department_Id] = @Department_Id
END