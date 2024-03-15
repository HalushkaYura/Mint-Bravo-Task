CREATE PROCEDURE [dbo].[sp_Log_DeleteLog]
    @LogId INT
AS
BEGIN
    DELETE FROM [dbo].[Log]
    WHERE [LogId] = @LogId;
END;