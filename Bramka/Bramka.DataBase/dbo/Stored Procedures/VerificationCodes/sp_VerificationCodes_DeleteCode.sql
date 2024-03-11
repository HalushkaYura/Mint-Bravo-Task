CREATE PROCEDURE [dbo].[sp_VerificationCodes_DeleteCode]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [dbo].[VerificationCodes]
    WHERE [UserId] = @UserId;
END;
