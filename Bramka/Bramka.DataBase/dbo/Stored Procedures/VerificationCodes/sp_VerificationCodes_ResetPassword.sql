CREATE PROCEDURE [dbo].[sp_VerificationCodes_ResetPassword]
	@Code nvarchar(255),
	@PasswordHash nvarchar(max)
AS
BEGIN
	DECLARE @UserId UNIQUEIDENTIFIER

	SELECT @UserId = UserId
	FROM [VerificationCodes]
	WHERE Code = @Code

	IF @UserId IS NOT NULL
	BEGIN
		UPDATE [User]
		SET PasswordHash = @PasswordHash
		WHERE UserId = @UserId

		DELETE FROM [VerificationCodes] WHERE UserId = @UserId AND Code = @Code
	END

	RETURN @@ROWCOUNT
END