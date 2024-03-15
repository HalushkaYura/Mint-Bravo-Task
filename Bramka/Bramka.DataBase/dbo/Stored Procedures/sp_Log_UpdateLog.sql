CREATE PROCEDURE [dbo].[sp_Log_UpdateLog]
    @LogId INT,
    @ActionType VARCHAR(255),
    @Description NVARCHAR(MAX),
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE [dbo].[Log]
    SET
        [ActionType] = @ActionType,
        [Description] = @Description,
        [UserId] = @UserId
    WHERE
        [LogId] = @LogId;
END;