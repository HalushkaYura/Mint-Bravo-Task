CREATE PROCEDURE [dbo].[sp_VerificationCodes_ConfirmEmail]
	@Code nvarchar(255)
AS
BEGIN
	DECLARE @UserId UNIQUEIDENTIFIER

	SELECT @UserId = UserId
	FROM [VerificationCodes]
	WHERE Code = @Code

	IF @UserId IS NOT NULL
	BEGIN
		UPDATE [User]
		SET EmailConfirmed = 1
		WHERE UserId = @UserId

		DELETE FROM [VerificationCodes] WHERE UserId = @UserId AND Code = @Code
	END

	RETURN @@ROWCOUNT
END
