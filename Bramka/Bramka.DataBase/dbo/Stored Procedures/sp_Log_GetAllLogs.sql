CREATE PROCEDURE [dbo].[sp_Log_GetAllLogs]
AS
BEGIN
    SELECT * FROM [dbo].[Log];
END;