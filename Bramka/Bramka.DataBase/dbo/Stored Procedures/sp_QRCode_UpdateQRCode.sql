CREATE PROCEDURE [dbo].[sp_QrCode_UpdateQrCode]
    @QrCodeId INT,
    @CodeHash NVARCHAR(Max),
    @GenerationCount Int,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE [dbo].[QrCode]
    SET
        [CodeHash] = @CodeHash,
        [UserId] = @UserId,
        [GenerationCount] = @GenerationCount
    WHERE
        [QrCodeId] = @QrCodeId;
END;