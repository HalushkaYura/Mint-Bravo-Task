CREATE PROCEDURE [dbo].[sp_Users_DeleteUser]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [dbo].[User]
    WHERE [UserId] = @UserId;
END;
