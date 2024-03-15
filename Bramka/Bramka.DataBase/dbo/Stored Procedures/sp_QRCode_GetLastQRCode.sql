CREATE PROCEDURE [dbo].[sp_QrCode_GetLastQrCode]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1 *
    FROM [dbo].[QrCode]
    WHERE [UserId] = @UserId
    ORDER BY [QrCodeId] DESC;
END;