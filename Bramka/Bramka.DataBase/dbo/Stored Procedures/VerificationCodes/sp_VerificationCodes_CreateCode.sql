CREATE PROCEDURE [dbo].[sp_VerificationCodes_CreateCode]
	@UserId uniqueidentifier,
	@Code nvarchar(255)
AS
BEGIN
	INSERT INTO [dbo].VerificationCodes (UserId, Code) VALUES (@UserId, @Code);
END
