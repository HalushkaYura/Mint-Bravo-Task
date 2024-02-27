CREATE PROCEDURE [dbo].[sp_Log_UpdateLog]
    @LogId INT,
    @ActionType VARCHAR(255),
    @Description NVARCHAR(MAX),
    @CreatedAt DATETIME,
    @QrCodeId INT,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE [dbo].[Log]
    SET
        [ActionType] = @ActionType,
        [Description] = @Description,
        [CreatedAt] = @CreatedAt,
        [QrCodeId] = @QrCodeId,
        [UserId] = @UserId
    WHERE
        [LogId] = @LogId;
END;