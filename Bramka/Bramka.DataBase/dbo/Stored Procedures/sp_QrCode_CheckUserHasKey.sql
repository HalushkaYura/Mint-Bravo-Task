CREATE PROCEDURE [dbo].[sp_QRCode_CheckUserHasKey]
    @UserId UNIQUEIDENTIFIER = NULL,
    @Code NVARCHAR(255)
AS
BEGIN
        SET NOCOUNT ON;
    IF @UserId IS NULL
    BEGIN
        SELECT * FROM [dbo].[QrCode]
        WHERE [UserId] IS NULL
        AND [CodeHash] = @Code;
    END
    ELSE
    BEGIN
        SELECT * FROM [dbo].[QrCode]
        WHERE [UserId] = @UserId
        AND [CodeHash] = @Code;
    END
END;
