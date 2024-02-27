CREATE PROCEDURE [dbo].[sp_Users_GetAllUsers]
AS
BEGIN
    SELECT * FROM [dbo].[User];
END;
