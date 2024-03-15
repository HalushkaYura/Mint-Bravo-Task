CREATE PROCEDURE [dbo].[GetLogByUserId]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM [dbo].[Log]
    WHERE [UserId] = @UserId;
END;