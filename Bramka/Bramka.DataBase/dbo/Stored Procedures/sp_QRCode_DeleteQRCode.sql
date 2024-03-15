CREATE PROCEDURE [dbo].[sp_QrCode_DeleteQrCode]
    @QrCodeId INT
AS
BEGIN
    DELETE FROM [dbo].[QrCode]
    WHERE [QrCodeId] = @QrCodeId;
END;