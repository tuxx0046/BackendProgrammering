﻿CREATE PROCEDURE [dbo].[spEmployee_GetAll]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [FirstName], [LastName], [Phone], [Position_Id], [Department_Id], [ApplicationUser_Id]
	FROM dbo.[Employee]
END