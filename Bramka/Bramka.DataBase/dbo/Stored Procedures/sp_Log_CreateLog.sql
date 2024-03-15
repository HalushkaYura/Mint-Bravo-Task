CREATE PROCEDURE [dbo].[sp_Log_CreateLog]
    @ActionType NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO [dbo].[Log] ([ActionType], [Description], [UserId])
    VALUES (@ActionType, @Description,  @UserId);
END;