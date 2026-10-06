CREATE OR ALTER PROCEDURE dbo.usp_Category_GetById
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Description, IsActive, CreatedAtUtc FROM dbo.Category WHERE Id=@Id;
END;
GO
