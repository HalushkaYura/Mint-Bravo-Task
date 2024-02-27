CREATE PROCEDURE [dbo].[sp_Log_GetLastLog]
AS
BEGIN
    SELECT TOP 1 * FROM [dbo].[Log]
    ORDER BY [CreatedAt] DESC;
END;