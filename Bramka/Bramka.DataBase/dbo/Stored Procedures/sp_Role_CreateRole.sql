CREATE PROCEDURE [dbo].[sp_Role_CreateRole]
    @Name NVARCHAR(100)
AS
BEGIN
    INSERT INTO [dbo].[Role] ([Name])
    VALUES (@Name);
END;