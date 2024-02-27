CREATE PROCEDURE [dbo].[sp_QrCode_UpdateUseDate]
    @QrCodeId INT,
    @UseDate DATETIME
AS
BEGIN
    UPDATE [dbo].[QrCode]
    SET [UseDate] = @UseDate
    WHERE [QrCodeId] = @QrCodeId;
END;
