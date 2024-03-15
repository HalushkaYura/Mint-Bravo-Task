CREATE PROCEDURE [dbo].[sp_QrCode_CreateQrCode]
    @CodeHash NVARCHAR(Max),
    @UserId UNIQUEIDENTIFIER = NULL,
    @QrCodeId INT OUTPUT
AS
BEGIN
    INSERT INTO [dbo].[QrCode] ([CodeHash], [UserId])
    VALUES ( @CodeHash, @UserId);

    SET @QrCodeId = SCOPE_IDENTITY();
END;