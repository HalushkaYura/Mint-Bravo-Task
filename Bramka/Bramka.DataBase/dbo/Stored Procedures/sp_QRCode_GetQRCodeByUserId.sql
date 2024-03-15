CREATE PROCEDURE [dbo].[sp_QrCode_GetQrCodeByUserId]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM [dbo].[QrCode]
    WHERE [UserId] = @UserId;
END;
