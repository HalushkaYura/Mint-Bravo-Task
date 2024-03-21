CREATE TABLE [dbo].[User]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY ,     
    [Name] NVARCHAR(100) NOT NULL, 
    [Surname] NVARCHAR(100) NOT NULL, 
    [BirthDate] DATETIME NOT NULL, 
    [PasswordHash] NVARCHAR(MAX) NOT NULL, 
    [Email] NVARCHAR(100) NOT NULL, 
    [EmailConfirmed] BIT NULL DEFAULT 0,
    [PhoneNumber] NVARCHAR(20) NULL, 
    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(), 
    [RefreshToken] NVARCHAR(MAX) NULL, 
    [TokenCreated] DATETIME NULL, 
    [TokenExpires] DATETIME NULL, 
    [RoleId] INT NOT NULL,


    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId]) ON DELETE CASCADE
);
