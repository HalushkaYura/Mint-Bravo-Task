CREATE PROCEDURE [dbo].[sp_Users_GetUserById]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM [dbo].[User]
    WHERE [UserId] = @UserId;
END;
