CREATE PROCEDURE [dbo].[sp_Role_GetRoleById]
    @RoleId INT
AS
BEGIN
    SELECT * FROM [dbo].[Role]
    WHERE [RoleId] = @RoleId;
END;