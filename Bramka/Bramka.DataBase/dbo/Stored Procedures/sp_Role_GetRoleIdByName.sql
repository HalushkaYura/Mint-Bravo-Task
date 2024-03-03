CREATE PROCEDURE [dbo].[sp_Role_GetRoleIdByName]
    @Name NVARCHAR(100)
AS
BEGIN
    SELECT [RoleId]
    FROM [dbo].[Role]
    WHERE LOWER([Name]) = LOWER(@Name);
END;
