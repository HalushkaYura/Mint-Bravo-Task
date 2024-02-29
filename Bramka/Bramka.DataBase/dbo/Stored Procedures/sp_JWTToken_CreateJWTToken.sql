CREATE PROCEDURE [dbo].[sp_JWTToken_CreateJWTToken]
    @Token NVARCHAR(MAX),
    @ExpirationDate DATETIME,
    @UserId UNIQUEIDENTIFIER
    
AS
BEGIN
    INSERT INTO [dbo].[JWTToken] ([Token], [ExpirationDate], [UserId], [CreatedAt])
    VALUES (@Token, @ExpirationDate, @UserId, GETDATE());
END;