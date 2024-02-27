CREATE PROCEDURE [dbo].[sp_JWTToken_UpdateJWTToken]
    @JWTTokenId INT,
    @Token NVARCHAR(MAX),
    @ExpirationDate DATETIME,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE [dbo].[JWTToken]
    SET
        [Token] = @Token,
        [ExpirationDate] = @ExpirationDate,
        [UserId] = @UserId
    WHERE
        [TokenId] = @JWTTokenId;
END;