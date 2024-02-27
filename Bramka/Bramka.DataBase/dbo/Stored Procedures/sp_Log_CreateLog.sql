CREATE PROCEDURE [dbo].[sp_Log_CreateLog]
    @ActionType NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @CreatedAt DATETIME,
    @QrCodeId INT,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO [dbo].[Log] ([ActionType], [Description], [CreatedAt], [QrCodeId], [UserId])
    VALUES (@ActionType, @Description, @CreatedAt, @QrCodeId, @UserId);
END;