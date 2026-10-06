CREATE OR ALTER PROCEDURE dbo.usp_Outbox_MarkPublished
    @Id UNIQUEIDENTIFIER,
    @PublishedAtUtc DATETIME2(7)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.OutboxMessage SET PublishedAtUtc=@PublishedAtUtc WHERE Id=@Id AND PublishedAtUtc IS NULL;
END;
GO
