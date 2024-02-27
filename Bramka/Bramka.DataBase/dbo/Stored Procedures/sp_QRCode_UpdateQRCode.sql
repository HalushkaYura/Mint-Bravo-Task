CREATE PROCEDURE [dbo].[sp_QrCode_UpdateQrCode]
    @QrCodeId INT,
    @Type NVARCHAR(255),
    @Code NVARCHAR(255),
    @ExpirationDate DATETIME,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE [dbo].[QrCode]
    SET
        [Type] = @Type,
        [Code] = @Code,
        [UserId] = @UserId,
        [ExpirationDate] = @ExpirationDate
    WHERE
        [QrCodeId] = @QrCodeId;
END;