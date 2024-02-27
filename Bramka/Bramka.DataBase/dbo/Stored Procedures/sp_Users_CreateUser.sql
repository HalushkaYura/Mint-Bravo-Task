CREATE PROCEDURE [dbo].[sp_Users_CreateUser]
    @Name NVARCHAR(100),
    @Surname NVARCHAR(100),
    @PasswordHash NVARCHAR(MAX),
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(20),
    @CreatedAt DATE,
    @RoleId INT,
    @UserId uniqueidentifier OUTPUT
AS
BEGIN
    SET @UserId = NEWID(); 
    INSERT INTO [dbo].[User] ([UserId], [Name], [Surname], [PasswordHash], [Email], [PhoneNumber], [CreatedAt], [RoleId])
    VALUES (@UserId, @Name, @Surname, @PasswordHash, @Email, @PhoneNumber, @CreatedAt, @RoleId);
END;