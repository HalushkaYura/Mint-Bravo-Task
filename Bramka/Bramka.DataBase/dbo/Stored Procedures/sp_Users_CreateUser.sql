CREATE PROCEDURE [dbo].[sp_Users_CreateUser]
    @Name NVARCHAR(100),
    @Surname NVARCHAR(100),
    @PasswordHash NVARCHAR(MAX),
    @BirthDate DateTime,
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(20),
    @RoleId INT,
    @UserId uniqueidentifier OUTPUT
AS
BEGIN
    SET @UserId = NEWID(); 
    INSERT INTO [dbo].[User] ([UserId], [Name], [Surname],[BirthDate], [PasswordHash], [Email], [PhoneNumber], [RoleId])
    VALUES (@UserId, @Name, @Surname, @BirthDate,@PasswordHash, @Email, @PhoneNumber, @RoleId);
END;