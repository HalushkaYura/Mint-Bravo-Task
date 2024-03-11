CREATE PROCEDURE [dbo].[sp_VerificationCodes_GetCodeByUserId]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT UserId, Code, CreatedAt FROM [dbo].VerificationCodes WHERE UserId = @UserId
END