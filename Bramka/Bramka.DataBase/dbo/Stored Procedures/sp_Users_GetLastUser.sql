CREATE PROCEDURE [dbo].[sp_Users_GetLastUser]
AS
BEGIN
    SELECT TOP 1 * FROM [dbo].[User]
    ORDER BY [CreatedAt] DESC;
END;
