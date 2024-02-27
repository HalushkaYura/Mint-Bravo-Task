CREATE PROCEDURE [dbo].[sp_JWTToken_DeleteJWTToken]
    @JWTTokenId INT
AS
BEGIN
    DELETE FROM [dbo].[JWTToken]
    WHERE [TokenId] = @JWTTokenId;
END;