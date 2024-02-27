CREATE PROCEDURE [dbo].[sp_JWTToken_GetJWTTokenById]
    @JWTTokenId INT
AS
BEGIN
    SELECT * FROM [dbo].[JWTToken]
    WHERE [TokenId] = @JWTTokenId;
END;