CREATE PROCEDURE [dbo].[sp_Users_GetUserByEmail]
	@Email nvarchar(100)
AS
BEGIN
	SELECT * FROM [dbo].[User] WHERE [Email] = @Email
END
