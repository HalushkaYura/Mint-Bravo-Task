CREATE PROCEDURE [dbo].[sp_Role_DeleteRole]
    @RoleId INT
AS
BEGIN
    DELETE FROM [dbo].[Role]
    WHERE [RoleId] = @RoleId;
END;