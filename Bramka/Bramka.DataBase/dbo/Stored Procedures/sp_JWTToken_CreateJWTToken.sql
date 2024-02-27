CREATE PROCEDURE [dbo].[sp_JWTToken_CreateJWTToken]
    @Token NVARCHAR(MAX),
    @ExpirationDate DATETIME,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO [dbo].[JWTToken] ([Token], [ExpirationDate], [UserId])
    VALUES (@Token, @ExpirationDate, @UserId);
END;