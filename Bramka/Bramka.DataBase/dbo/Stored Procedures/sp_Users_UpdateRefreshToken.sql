CREATE PROCEDURE [dbo].[sp_Users_UpdateRefreshToken]
    @UserId UNIQUEIDENTIFIER,
    @Email NVARCHAR(100),
    @RefreshToken NVARCHAR(MAX),
    @TokenCreated DATETIME,
    @TokenExpires DATETIME
AS
	UPDATE [dbo].[User]
    SET
        [RefreshToken] = @RefreshToken,
        [TokenCreated] = @TokenCreated,
        [TokenExpires] = @TokenExpires
    WHERE
        [UserId] = @UserId AND [Email] = @Email;
RETURN 0
