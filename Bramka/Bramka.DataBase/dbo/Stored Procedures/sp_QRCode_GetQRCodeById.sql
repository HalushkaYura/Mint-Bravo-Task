CREATE PROCEDURE [dbo].[sp_QrCode_GetQrCodeById]
    @QrCodeId INT
AS
BEGIN
    SELECT * FROM [dbo].[QrCode]
    WHERE [QrCodeId] = @QrCodeId;
END;