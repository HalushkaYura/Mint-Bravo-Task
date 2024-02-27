CREATE PROCEDURE [dbo].[sp_QrCode_CreateQrCode]
    @Type NVARCHAR(255),
    @Code NVARCHAR(255),
    @ExpirationDate DATETIME,
    @UserId UNIQUEIDENTIFIER,
    @IdQrCode INT OUTPUT
AS
BEGIN
    INSERT INTO [dbo].[QrCode] ([Type], [Code], [ExpirationDate], [UserId])
    VALUES (@Type, @Code, @ExpirationDate, @UserId);

    SET @IdQrCode = SCOPE_IDENTITY();
END;