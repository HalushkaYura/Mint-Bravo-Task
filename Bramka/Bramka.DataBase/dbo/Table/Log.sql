CREATE TABLE [dbo].[Log]
(
	[LogId] INT NOT NULL PRIMARY KEY IDENTITY (1, 1), 
    [ActionType] NVARCHAR(255)  NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [CreatedAt] DATETIME NOT NULL, 
    [QrCodeId] INT NULL, 
    [UserId] UNIQUEIDENTIFIER NULL,

    FOREIGN KEY ([QrCodeId]) REFERENCES [dbo].[QrCode]([QrCodeId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]) ON DELETE CASCADE
);
