CREATE PROCEDURE sp_Users_CheckExistEmail
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM [dbo].[User]
    WHERE [Email] = @Email;
END