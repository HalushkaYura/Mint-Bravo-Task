CREATE PROCEDURE [dbo].[sp_QrCode_GetQrCodeById]
    @QrCodeId INT
AS
BEGIN

    UPDATE [dbo].[QrCode]
    SET [GenerationCount] +=  1
    WHERE [QrCodeId] = @QrCodeId;

    SELECT * FROM [dbo].[QrCode]
    WHERE [QrCodeId] = @QrCodeId;
END;