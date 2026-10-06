CREATE OR ALTER PROCEDURE dbo.usp_OrderOutbox_MarkPublished
    @Id UNIQUEIDENTIFIER,
    @PublishedAtUtc DATETIME2(7)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.OrderOutboxMessage
    SET PublishedAtUtc = @PublishedAtUtc
    WHERE Id = @Id
      AND PublishedAtUtc IS NULL;
END;
GO
