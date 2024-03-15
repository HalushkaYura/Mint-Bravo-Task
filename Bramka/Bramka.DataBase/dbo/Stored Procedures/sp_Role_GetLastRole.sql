CREATE PROCEDURE [dbo].[sp_Role_GetLastRole]
AS
BEGIN
    SELECT TOP 1 * FROM [dbo].[Role]
    ORDER BY [RoleId] DESC;
END;