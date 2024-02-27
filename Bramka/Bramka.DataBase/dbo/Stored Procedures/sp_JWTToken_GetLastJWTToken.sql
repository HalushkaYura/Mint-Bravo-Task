CREATE PROCEDURE [dbo].[sp_JWTToken_GetLastJWTToken]
AS
BEGIN
    SELECT TOP 1 * FROM [dbo].[JWTToken]
    ORDER BY [ExpirationDate] DESC;
END;