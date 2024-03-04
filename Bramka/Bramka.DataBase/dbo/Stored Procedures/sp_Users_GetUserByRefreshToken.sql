CREATE PROCEDURE [dbo].[sp_Users_GetUserByRefreshToken]
	@RefreshToken nvarchar(MAX)
AS
BEGIN
	SELECT * FROM [dbo].[User] WHERE [RefreshToken] = @RefreshToken
END
