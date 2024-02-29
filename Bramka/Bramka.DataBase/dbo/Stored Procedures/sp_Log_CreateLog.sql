CREATE PROCEDURE [dbo].[sp_Log_CreateLog]
    @ActionType NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @QrCodeId INT,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO [dbo].[Log] ([ActionType], [Description], [CreatedAt], [QrCodeId], [UserId])
    VALUES (@ActionType, @Description, GETDATE(), @QrCodeId, @UserId);
END;