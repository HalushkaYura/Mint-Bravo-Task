CREATE PROCEDURE [dbo].[sp_Log_GetLogById]
    @LogId INT
AS
BEGIN
    SELECT * FROM [dbo].[Log]
    WHERE [LogId] = @LogId;
END;