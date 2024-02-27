CREATE PROCEDURE [dbo].[sp_Role_UpdateRole]
    @RoleId INT,
    @Name NVARCHAR(100)
AS
BEGIN
    UPDATE [dbo].[Role]
    SET [Name] = @Name
    WHERE [RoleId] = @RoleId;
END;