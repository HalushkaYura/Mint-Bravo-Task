CREATE PROCEDURE [dbo].[sp_QrCode_GetAllQrCodes]
AS
BEGIN
    SELECT * FROM [dbo].[QrCode];
END;