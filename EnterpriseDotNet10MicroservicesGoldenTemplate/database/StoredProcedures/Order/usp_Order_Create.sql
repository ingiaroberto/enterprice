CREATE OR ALTER PROCEDURE dbo.usp_Order_Create
    @Id UNIQUEIDENTIFIER,
    @CategoryId UNIQUEIDENTIFIER,
    @CreatedAtUtc DATETIME2(7)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.[Order](Id, CategoryId, CreatedAtUtc)
    VALUES (@Id, @CategoryId, @CreatedAtUtc);
END;
GO
