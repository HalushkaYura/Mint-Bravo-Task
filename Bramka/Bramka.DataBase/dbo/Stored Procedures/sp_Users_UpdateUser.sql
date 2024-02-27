﻿CREATE PROCEDURE [dbo].[sp_Users_UpdateUser]
    @UserId UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @Surname NVARCHAR(100),
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(20),
    @RoleId INT
AS
BEGIN
    UPDATE [dbo].[User]
    SET
        [Name] = @Name,
        [Surname] = @Surname,
        [Email] = @Email,
        [PhoneNumber] = @PhoneNumber,
        [RoleId] = @RoleId
    WHERE
        [UserId] = @UserId;
END;
