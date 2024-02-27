CREATE PROCEDURE [dbo].[sp_JWTToken_GetAllJWTTokens]
AS
BEGIN
    SELECT * FROM [dbo].[JWTToken];
END;