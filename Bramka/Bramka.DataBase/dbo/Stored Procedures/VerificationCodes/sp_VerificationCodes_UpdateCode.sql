CREATE PROCEDURE [dbo].[sp_VerificationCodes_UpdateCode]
	@UserId UNIQUEIDENTIFIER,
	@Code NVARCHAR(255)
AS
BEGIN
	UPDATE VerificationCodes
    SET Code = @Code,
        CreatedAt = GETDATE()
    WHERE UserId = @UserId;
END
